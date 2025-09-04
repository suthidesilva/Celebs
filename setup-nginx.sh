#!/bin/bash

echo "🚀 Setting up Nginx Reverse Proxy for Celeb App..."

# Check if nginx is installed
if ! command -v nginx &> /dev/null; then
    echo "📦 Installing nginx..."
    sudo apt-get update
    sudo apt-get install -y nginx
else
    echo "✅ Nginx is already installed"
fi

# Create nginx configuration
echo "⚙️  Creating nginx configuration..."
sudo tee /etc/nginx/sites-available/celeb-app > /dev/null << 'EOF'
server {
    listen 80;
    server_name localhost;

    # API requests go to .NET backend (port 5555)
    location /api/ {
        proxy_pass http://localhost:5555;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }

    # All other requests go to Angular app (port 4200)
    location / {
        proxy_pass http://localhost:4200;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}
EOF

# Enable the site
echo "🔗 Enabling the site..."
sudo ln -sf /etc/nginx/sites-available/celeb-app /etc/nginx/sites-enabled/

# Remove default site
sudo rm -f /etc/nginx/sites-enabled/default

# Test nginx configuration
echo "🧪 Testing nginx configuration..."
sudo nginx -t

if [ $? -eq 0 ]; then
    echo "✅ Nginx configuration is valid"
    
    # Restart nginx
    echo "🔄 Restarting nginx..."
    sudo systemctl restart nginx
    
    echo "🎉 Setup complete! Nginx is now running on port 80"
    echo "📡 Routes:"
    echo "   - /api/* → .NET Backend (port 5555)"
    echo "   - /* → Angular App (port 4200)"
    echo ""
    echo "🚀 Make sure to:"
    echo "   1. Start your .NET backend: cd CelebApi && dotnet run"
    echo "   2. Start your Angular app: cd celeb-angular && npm start"
    echo "   3. Access your app at: http://localhost"
else
    echo "❌ Nginx configuration test failed"
    exit 1
fi
