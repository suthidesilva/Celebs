import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Celeb } from '../../models/celeb';

@Component({
  selector: 'app-celeb-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './celeb-card.component.html',
  styleUrls: ['./celeb-card.component.css']
})
export class CelebCardComponent {
  @Input() celeb!: Celeb;
  @Input() viewMode: 'grid' | 'list' = 'grid';
  @Output() deleteCeleb = new EventEmitter<string>();

  constructor(private router: Router) {}

  onDelete(event: Event): void {
    event.stopPropagation();
    this.deleteCeleb.emit(this.celeb.id);
  }

  onCardClick(): void {
    this.router.navigate(['/celeb', this.celeb.id]);
  }
}
