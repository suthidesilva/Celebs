import { Component, ElementRef, ViewChild, AfterViewInit, OnDestroy, HostListener } from '@angular/core';

@Component({
  selector: 'app-particle-background',
  standalone: true,
  templateUrl: './particle-background.component.html',
  styleUrls: ['./particle-background.component.css']
})
export class ParticleBackgroundComponent implements AfterViewInit, OnDestroy {
  @ViewChild('particleCanvas', { static: true }) canvasRef!: ElementRef<HTMLCanvasElement>;
  
  private canvas!: HTMLCanvasElement;
  private ctx!: CanvasRenderingContext2D;
  private particles: Particle[] = [];
  private animationId!: number;
  private mouseX = 0;
  private mouseY = 0;

  constructor() {
    console.log('ParticleBackgroundComponent: Constructor called');
  }

  ngAfterViewInit() {
    console.log('ParticleBackgroundComponent: ngAfterViewInit called');
    this.canvas = this.canvasRef.nativeElement;
    this.ctx = this.canvas.getContext('2d')!;
    console.log('ParticleBackgroundComponent: Canvas and context initialized');
    this.setupCanvas();
    this.createParticles();
    this.animate();
  }

  ngOnDestroy() {
    if (this.animationId) {
      cancelAnimationFrame(this.animationId);
    }
  }

  @HostListener('window:resize')
  onResize() {
    this.setupCanvas();
    this.createParticles();
  }

  @HostListener('mousemove', ['$event'])
  onMouseMove(event: MouseEvent) {
    this.mouseX = event.clientX;
    this.mouseY = event.clientY;
  }

  private setupCanvas() {
    this.canvas.width = window.innerWidth;
    this.canvas.height = window.innerHeight;
  }

  private createParticles() {
    this.particles = [];
    const particleCount = 150;
    
    for (let i = 0; i < particleCount; i++) {
      this.particles.push(new Particle(
        Math.random() * this.canvas.width,
        Math.random() * this.canvas.height,
        this.canvas.width,
        this.canvas.height
      ));
    }
  }

  private animate() {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    
    this.particles.forEach(particle => {
      particle.update(this.mouseX, this.mouseY);
      particle.draw(this.ctx);
    });
    
    this.animationId = requestAnimationFrame(() => this.animate());
  }
}

class Particle {
  x: number;
  y: number;
  radius: number;
  dx: number;
  dy: number;
  private canvasWidth: number;
  private canvasHeight: number;

  constructor(x: number, y: number, canvasWidth: number, canvasHeight: number) {
    this.x = x;
    this.y = y;
    this.radius = Math.random() * 3 + 1; // Smaller particles (1-4px)
    this.dx = Math.random() * 2 - 1; // Slower movement
    this.dy = Math.random() * 2 - 1;
    this.canvasWidth = canvasWidth;
    this.canvasHeight = canvasHeight;
  }

  update(mouseX: number, mouseY: number) {
    // Mouse interaction
    const dx = mouseX - this.x;
    const dy = mouseY - this.y;
    const distance = Math.sqrt(dx * dx + dy * dy);
    
    if (distance < 100) {
      const force = (100 - distance) / 100;
      this.dx += (dx / 50) * force;
      this.dy += (dy / 50) * force;
    }

    // Update position
    this.x += this.dx;
    this.y += this.dy;

    // Bounce off walls
    if (this.x + this.radius > this.canvasWidth || this.x - this.radius < 0) {
      this.dx *= -0.8; // Add some damping
      this.x = Math.max(this.radius, Math.min(this.canvasWidth - this.radius, this.x));
    }
    if (this.y + this.radius > this.canvasHeight || this.y - this.radius < 0) {
      this.dy *= -0.8; // Add some damping
      this.y = Math.max(this.radius, Math.min(this.canvasHeight - this.radius, this.y));
    }

    // Add some damping to prevent infinite acceleration
    this.dx *= 0.99;
    this.dy *= 0.99;
  }

  draw(ctx: CanvasRenderingContext2D) {
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
    ctx.fillStyle = 'rgba(0, 0, 0, 0.3)'; // Semi-transparent black
    ctx.fill();
    ctx.closePath();
  }
}
