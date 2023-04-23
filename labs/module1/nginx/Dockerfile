# Simple Dockerfile for NGINX

FROM nginx:stable-alpine

LABEL author="sampleauthor@contoso.com"
LABEL version="1.0"

COPY index.html /usr/share/nginx/html/index.html

CMD ["nginx", "-g", "daemon off;"]