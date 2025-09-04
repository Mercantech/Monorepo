#%% 
# Import pakker
import yfinance as yf
import pandas as pd
from datetime import date, timedelta
from matplotlib import pyplot as plt
import numpy as np
# Importer TensorFlow
import tensorflow as tf
from tensorflow import keras
from tensorflow.keras import layers
# Importer nødvendige biblioteker
from sklearn.model_selection import train_test_split
from tensorflow.keras.callbacks import EarlyStopping  # Importer EarlyStopping

#Tjek TensorFlow version
print(tf.__version__)

# Tjek om GPU er tilgængelig og sæt det som en enhed
gpus = tf.config.experimental.list_physical_devices('GPU')

try:
    # Tildel GPU-minne dynamisk
    for gpu in gpus:
        tf.config.experimental.set_memory_growth(gpu, True)
    print(f"Tilgængelige GPU'er: {gpus}")
except RuntimeError as e:
    print(e)  # Udsender fejl, hvis der opstår et problem

#%%
# Indlæs tidspunkter for træningsdata som består af 5 års aktiekurser
Start_train = date.today() - timedelta(1825)
Start_train.strftime('%Y-%m-%d')

End_train = date.today() - timedelta(366)
End_train.strftime('%Y-%m-%d')

#%%
# Indlæs tidspunkter for testdata som består af 1 år aktiekurser
Start_test = date.today() - timedelta(365)
Start_test.strftime('%Y-%m-%d')

End_test = date.today() + timedelta(2)
End_test.strftime('%Y-%m-%d')

#%%
# Funktion til at indlæse data
def closing_price(tickers, Start, End):
    Asset = pd.DataFrame()
    for ticker in tickers:
        data = yf.download(ticker, start=Start, end=End)
        Asset[ticker] = data['Adj Close']
    return Asset

# Funktion til at normalisere aktiekurser
def normalize_data(data):
    return (data / data.iloc[0]) * 100

# Funktion til at hente regnskabsdata
def get_financials(tickers):
    financials = {}
    for ticker in tickers:
        financials[ticker] = yf.Ticker(ticker).financials
    return financials

# Funktion til at hente nyheder
def get_news(tickers):
    news = {}
    for ticker in tickers:
        news[ticker] = yf.Ticker(ticker).news
    return news

# Funktion til at forudsige aktiekurser med TensorFlow
def predict_stock_prices(data):
    predictions = {}
    evaluation_metrics = {}
    
    for ticker in data.columns:
        y = data[ticker].values  # Aktiekurser som afhængig variabel
        X = np.array(range(len(data))).reshape(-1, 1)  # Tid som uafhængig variabel
        
        # Opdel data i trænings- og testdata
        X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
        
        # Byg modellen
        model = keras.Sequential([
            layers.Dense(32, activation='relu', input_shape=(1,)),
            layers.Dense(32, activation='relu'),
            layers.Dense(1)  # Output lag
        ])
        
        model.compile(optimizer='adam', loss='mean_squared_error')
        
        # Implementer Early Stopping
        early_stopping = EarlyStopping(monitor='val_loss', patience=5, restore_best_weights=True)
        
        # Træn modellen med Early Stopping
        model.fit(X_train, y_train, epochs=3, batch_size=32, verbose=0, validation_split=0.2, callbacks=[early_stopping])
        
        # Forudsig næste periode
        next_period = np.array([[len(data)]])  # Næste tidsperiode
        predictions[ticker] = model.predict(next_period)[0][0]
        
        # Evaluering af modellen
        y_pred = model.predict(X_test)
        mse = np.mean((y_test - y_pred.flatten()) ** 2)  # Mean Squared Error
        mae = np.mean(np.abs(y_test - y_pred.flatten()))  # Mean Absolute Error
        evaluation_metrics[ticker] = {'MSE': mse, 'MAE': mae}
    
    return predictions, evaluation_metrics

# Rolling-window validation i TensorFlow
def rolling_window_predict_stock_prices(data, window_size=200):
    """
    data: En pandas- eller numpy-serie med aktiekurser for én enkelt aktie
    window_size: Antal datapunkter i træning pr. vindue
    
    Returnerer:
        predictions: Liste af forudsagte værdier
        actuals: Liste af faktiske værdier, vi forudsagde
        errors: Gennemsnitlig fejl (MSE) over alle vinduer
    """
    # Konverter til numpy-array, hvis ikke det allerede er det
    if isinstance(data, pd.Series) or isinstance(data, pd.DataFrame):
        prices = data.values
    else:
        prices = data
        
    # Her gemmes alle prediction og deres 'ground truth'
    predictions = []
    actuals = []
    
    # Loop over tidsvinduer
    for start_idx in range(0, len(prices) - window_size):
        end_idx = start_idx + window_size
        
        # Træningsdata
        X_train = np.array(range(start_idx, end_idx)).reshape(-1, 1)
        y_train = prices[start_idx:end_idx]
        
        # Den næste dag/time-step vi vil forudsige
        if end_idx >= len(prices):
            break
        
        X_test = np.array([[end_idx]])  # næste tidspunkt
        y_test = prices[end_idx]        # 'sand' pris på næste tidspunkt
        
        # Byg en ny model for hvert vindue
        model = keras.Sequential([
            layers.Dense(64, activation='relu', input_shape=(1,)),
            layers.Dense(64, activation='relu'),
            layers.Dense(1)
        ])
        
        model.compile(optimizer='adam', loss='mean_squared_error')
        
        # Træn modellen på rullende vindue
        model.fit(X_train, y_train, epochs=5, verbose=0)
        
        # Forudsig
        y_pred = model.predict(X_test)[0][0]
        
        # Gem prediction og actual
        predictions.append(y_pred)
        actuals.append(y_test)
    
    # Beregn en fejl for at vurdere performance (MSE som eksempel)
    errors = np.mean((np.array(predictions) - np.array(actuals))**2)
    return predictions, actuals, errors

def rolling_window_validation_all_tickers(dataframe, window_size=200):
    """
    Anvender rolling window på et DataFrame med flere tickers (kolonner).
    Returnerer en dict med resultater for hvert ticker.
    """
    results = {}
    for ticker in dataframe.columns:
        preds, actuals, mse = rolling_window_predict_stock_prices(
            dataframe[ticker], window_size
        )
        results[ticker] = {
            'predictions': preds,
            'actuals': actuals,
            'mse': mse
        }
    return results

# Indlæs data
tickers = ['MSFT', 'AAPL', 'GOOGL', 'AMZN', 'TSLA', 'NVDA', 'META', 'NFLX', 'ORCL', 'IBM', 'WMT', 'JNJ', 'VZ', 'BA', 'CAT', 'CSCO', 'MMM', 'PFE', 'PG', 'TRMB', 'V', 'WBA', 'DIS', 'IBM', 'JPM', 'KO', 'MCD', 'NKE', 'WMT', 'XOM']  
data = closing_price(tickers, Start_train, End_train) 
normalized_data = normalize_data(data)  # Normaliser data

#%%
# Hent og print regnskabsdata
financials = get_financials(tickers)
print(financials)

#%%
# Plot normaliseret data
plt.figure(figsize=(12, 8))  
plt.plot(normalized_data)
plt.legend(normalized_data.columns, bbox_to_anchor=(1.05, 1), loc='upper left')  # Placer legend uden for grafen
plt.title('Normaliserede Aktiekurser')
plt.xlabel('Dato')
plt.ylabel('Normaliseret Pris')
plt.tight_layout()  
plt.show()


#%% 
# Print sidste dato for træningsdata med % stigning sorteret efter stigning med deres fulde navn
print(normalized_data.iloc[-1].sort_values(ascending=False).to_string())

#%%
# Forudsig aktiekurserne for næste periode med TensorFlow uden rolling window
predicted_prices, evaluation_metrics = predict_stock_prices(normalized_data)
print(predicted_prices)
print(evaluation_metrics)  # Print evaluation metrics

#%%
# Forudsig aktiekurserne for næste periode med TensorFlow med rolling window
window_size = 200 
rolling_results = rolling_window_validation_all_tickers(normalized_data, window_size)

#%%
# Evaluer begge modeller
# Sammenlign resultaterne
for ticker in normalized_data.columns:
    plt.figure(figsize=(12, 6))
    
    # Plot faktiske priser
    plt.plot(normalized_data[ticker], label='Faktiske Priser', color='blue')
    
    # Plot forudsigelser fra den første model
    plt.axvline(x=len(normalized_data)-1, color='gray', linestyle='--', label='Forudsigelse Start')
    plt.scatter(len(normalized_data), predicted_prices[ticker], color='red', label='Forudsiget Pris (Uden Rolling Window')
    
    # Plot forudsigelser fra rolling window modellen
    rolling_pred = rolling_results[ticker]['predictions']
    plt.plot(range(len(normalized_data), len(normalized_data) + len(rolling_pred)), rolling_pred, color='orange', label='Forudsiget Pris (Med Rolling Window)')
    
    plt.title(f'Forudsigelse af Aktiekurser for {ticker}')
    plt.xlabel('Tid')
    plt.ylabel('Pris')
    plt.legend()
    plt.show()

# Print evaluering metrics
print("Evaluering Metrics for den første model:")
for ticker, metrics in evaluation_metrics.items():
    print(f"{ticker}: MSE = {metrics['MSE']}, MAE = {metrics['MAE']}")

print("\nEvaluering Metrics for rolling window modellen:")
for ticker, results in rolling_results.items():
    print(f"{ticker}: MSE = {results['mse']}")

#%% 

