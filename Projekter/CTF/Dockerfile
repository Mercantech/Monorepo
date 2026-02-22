# Brug nginx alpine image for minimal størrelse
FROM nginx:alpine

# Kopier statiske filer til nginx html mappe
COPY . /usr/share/nginx/html/

# Kopier nginx konfiguration
COPY nginx.conf /etc/nginx/nginx.conf

# Eksponer port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
