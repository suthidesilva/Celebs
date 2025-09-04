import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-celeb-toolbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './celeb-toolbar.component.html',
  styleUrls: ['./celeb-toolbar.component.css']
})
export class CelebToolbarComponent {
  @Input() viewMode: 'grid' | 'list' = 'grid';
  @Output() toggleView = new EventEmitter<void>();
  @Output() reset = new EventEmitter<void>();

  onToggleView(): void {
    this.toggleView.emit();
  }

  onReset(): void {
    this.reset.emit();
  }
}
