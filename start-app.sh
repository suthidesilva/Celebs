#!/bin/bash

echo "🚀 Starting Celeb App with Nginx Reverse Proxy..."

# Check if nginx is running
if ! pgrep -x "nginx" > /dev/null; then
    echo "📡 Starting nginx..."
    sudo nginx
else
    echo "✅ Nginx is already running"
fi



echo ""
echo "🎉 All services started!"
echo "📡 Access your app at: http://localhost"
echo "🔧 Backend API at: http://localhost/api/celebs"
echo ""
echo "Press Ctrl+C to stop all services"

# Function to cleanup on exit
cleanup() {
    echo ""
    echo "🛑 Stopping services..."
    kill $BACKEND_PID 2>/dev/null
    kill $ANGULAR_PID 2>/dev/null
    echo "✅ All services stopped"
    exit 0
}

# Set trap to cleanup on script exit
trap cleanup SIGINT SIGTERM

# Keep script running
wait
