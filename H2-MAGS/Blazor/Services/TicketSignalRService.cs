using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;

namespace Blazor.Services
{
    /// <summary>
    /// Service for SignalR kommunikation med ticket systemet
    /// Håndterer real-time chat og notifikationer
    /// </summary>
    public class TicketSignalRService : INotifyPropertyChanged, IAsyncDisposable
    {
        private HubConnection? _hubConnection;
        private readonly ILogger<TicketSignalRService> _logger;
        private readonly string _hubUrl;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<string, string, string, bool, DateTime>? MessageReceived;
        public event Action<string, string, DateTime>? UserJoined;
        public event Action<string, string, DateTime>? UserLeft;
        public event Action<string, bool, DateTime>? TypingIndicator;
        public event Action<string, string, string, DateTime>? StatusUpdated;
        public event Action<string, string, string, DateTime>? TicketAssigned;
        public event Action<string, string, string, DateTime>? TicketClosed;
        public event Action<string, string, string, string, DateTime>? CommentAdded;
        public event Action<string, string, string, string, DateTime>? TicketCreated;
        public event Action<string, string, string, DateTime>? TicketUpdated;
        public event Action<string>? Error;
        public event Action<string>? Connected;

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;
        public string? CurrentTicketId { get; private set; }
        public string? CurrentUserId { get; private set; }
        public string? CurrentUsername { get; private set; }
        public string? CurrentUserRole { get; private set; }

        public TicketSignalRService(ILogger<TicketSignalRService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _hubUrl = configuration["ApiBaseUrl"] + "/tickethub";
        }

        /// <summary>
        /// Initialiser SignalR forbindelse
        /// </summary>
        public async Task InitializeAsync(string token)
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                })
                .WithAutomaticReconnect()
                .Build();

            // Registrer event handlers
            RegisterEventHandlers();

            try
            {
                await _hubConnection.StartAsync();
                _logger.LogInformation("SignalR forbindelse etableret");
                OnPropertyChanged(nameof(IsConnected));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved etablering af SignalR forbindelse");
                Error?.Invoke("Kunne ikke forbinde til chat systemet");
            }
        }

        /// <summary>
        /// Tilslut til en ticket chat
        /// </summary>
        public async Task JoinTicketAsync(string ticketId, string userId, string username, string userRole)
        {
            if (_hubConnection?.State != HubConnectionState.Connected)
            {
                Error?.Invoke("Ikke forbundet til chat systemet");
                return;
            }

            try
            {
                CurrentTicketId = ticketId;
                CurrentUserId = userId;
                CurrentUsername = username;
                CurrentUserRole = userRole;

                await _hubConnection.InvokeAsync("JoinTicketGroup", ticketId);
                _logger.LogInformation("Tilsluttet til ticket {TicketId}", ticketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved tilslutning til ticket {TicketId}", ticketId);
                Error?.Invoke("Kunne ikke tilslutte til ticket chat");
            }
        }

        /// <summary>
        /// Forlad ticket chat
        /// </summary>
        public async Task LeaveTicketAsync()
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("LeaveTicketGroup", CurrentTicketId);
                _logger.LogInformation("Forladt ticket {TicketId}", CurrentTicketId);
                
                CurrentTicketId = null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved forladelse af ticket {TicketId}", CurrentTicketId);
            }
        }

        /// <summary>
        /// Send besked til ticket chat
        /// </summary>
        public async Task SendMessageAsync(string message, bool isInternal = false)
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                Error?.Invoke("Ikke forbundet til chat systemet");
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("SendMessage", CurrentTicketId, message, isInternal);
                _logger.LogInformation("Besked sendt til ticket {TicketId}", CurrentTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved afsendelse af besked til ticket {TicketId}", CurrentTicketId);
                Error?.Invoke("Kunne ikke sende besked");
            }
        }

        /// <summary>
        /// Send typing indikator
        /// </summary>
        public async Task SendTypingIndicatorAsync(bool isTyping)
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("SendTypingIndicator", CurrentTicketId, isTyping);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved afsendelse af typing indikator");
            }
        }

        /// <summary>
        /// Send status opdatering
        /// </summary>
        public async Task SendStatusUpdateAsync(string status, string message)
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                Error?.Invoke("Ikke forbundet til chat systemet");
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("SendStatusUpdate", CurrentTicketId, status, message);
                _logger.LogInformation("Status opdatering sendt for ticket {TicketId}", CurrentTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved afsendelse af status opdatering");
                Error?.Invoke("Kunne ikke opdatere status");
            }
        }

        /// <summary>
        /// Send ticket tildeling notifikation
        /// </summary>
        public async Task SendAssignmentNotificationAsync(string assigneeId, string assigneeName)
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                Error?.Invoke("Ikke forbundet til chat systemet");
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("SendAssignmentNotification", CurrentTicketId, assigneeId, assigneeName);
                _logger.LogInformation("Tildeling notifikation sendt for ticket {TicketId}", CurrentTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved afsendelse af tildeling notifikation");
                Error?.Invoke("Kunne ikke sende tildeling notifikation");
            }
        }

        /// <summary>
        /// Send ticket lukning notifikation
        /// </summary>
        public async Task SendTicketClosedNotificationAsync(string resolution, string closedBy)
        {
            if (_hubConnection?.State != HubConnectionState.Connected || string.IsNullOrEmpty(CurrentTicketId))
            {
                Error?.Invoke("Ikke forbundet til chat systemet");
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("SendTicketClosedNotification", CurrentTicketId, resolution, closedBy);
                _logger.LogInformation("Ticket lukning notifikation sendt for ticket {TicketId}", CurrentTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved afsendelse af ticket lukning notifikation");
                Error?.Invoke("Kunne ikke sende lukning notifikation");
            }
        }

        /// <summary>
        /// Registrer event handlers for SignalR events
        /// </summary>
        private void RegisterEventHandlers()
        {
            if (_hubConnection == null) return;

            _hubConnection.On<string, string, string, bool, DateTime>("MessageReceived", (id, ticketId, message, isInternal, timestamp) =>
            {
                MessageReceived?.Invoke(id, ticketId, message, isInternal, timestamp);
            });

            _hubConnection.On<string, string, DateTime>("UserJoined", (username, userId, timestamp) =>
            {
                UserJoined?.Invoke(username, userId, timestamp);
            });

            _hubConnection.On<string, string, DateTime>("UserLeft", (username, userId, timestamp) =>
            {
                UserLeft?.Invoke(username, userId, timestamp);
            });

            _hubConnection.On<string, bool, DateTime>("TypingIndicator", (username, isTyping, timestamp) =>
            {
                TypingIndicator?.Invoke(username, isTyping, timestamp);
            });

            _hubConnection.On<string, string, string, DateTime>("StatusUpdated", (ticketId, status, message, timestamp) =>
            {
                StatusUpdated?.Invoke(ticketId, status, message, timestamp);
            });

            _hubConnection.On<string, string, string, DateTime>("TicketAssigned", (ticketId, assigneeId, assigneeName, timestamp) =>
            {
                TicketAssigned?.Invoke(ticketId, assigneeId, assigneeName, timestamp);
            });

            _hubConnection.On<string, string, string, DateTime>("TicketClosed", (ticketId, resolution, closedBy, timestamp) =>
            {
                TicketClosed?.Invoke(ticketId, resolution, closedBy, timestamp);
            });

            _hubConnection.On<string, string, string, string, DateTime>("CommentAdded", (commentId, ticketId, message, authorName, timestamp) =>
            {
                CommentAdded?.Invoke(commentId, ticketId, message, authorName, timestamp);
            });

            _hubConnection.On<string, string, string, string, DateTime>("TicketCreated", (ticketId, ticketNumber, title, serviceType, timestamp) =>
            {
                TicketCreated?.Invoke(ticketId, ticketNumber, title, serviceType, timestamp);
            });

            _hubConnection.On<string, string, string, DateTime>("TicketUpdated", (ticketId, status, priority, timestamp) =>
            {
                TicketUpdated?.Invoke(ticketId, status, priority, timestamp);
            });

            _hubConnection.On<string>("Error", (message) =>
            {
                Error?.Invoke(message);
            });

            _hubConnection.On<string>("Connected", (message) =>
            {
                Connected?.Invoke(message);
            });

            _hubConnection.On<string>("JoinedTicket", (message) =>
            {
                // Handle joined ticket confirmation
            });

            // Connection state changed events
            _hubConnection.Closed += async (error) =>
            {
                _logger.LogWarning("SignalR forbindelse lukket: {Error}", error?.Message);
                OnPropertyChanged(nameof(IsConnected));
                
                if (error != null)
                {
                    Error?.Invoke("Forbindelse til chat systemet mistet");
                }
            };

            _hubConnection.Reconnected += async (connectionId) =>
            {
                _logger.LogInformation("SignalR forbindelse genoprettet: {ConnectionId}", connectionId);
                OnPropertyChanged(nameof(IsConnected));
            };

            _hubConnection.Reconnecting += async (error) =>
            {
                _logger.LogWarning("SignalR forbindelse genoprettes: {Error}", error?.Message);
                OnPropertyChanged(nameof(IsConnected));
            };
        }

        /// <summary>
        /// Trigger PropertyChanged event
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Dispose SignalR forbindelse
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }
}
