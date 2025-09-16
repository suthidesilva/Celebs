import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-celeb-toolbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './celeb-toolbar.component.html',
  styleUrls: ['./celeb-toolbar.component.css']
})
export class CelebToolbarComponent {
  @Input() viewMode: 'grid' | 'list' = 'grid';
  @Output() toggleView = new EventEmitter<void>();
  @Output() reset = new EventEmitter<void>();
  @Output() searchChange = new EventEmitter<string>();

  searchTerm = '';

  onToggleView(): void {
    this.toggleView.emit();
  }

  onReset(): void {
    this.reset.emit();
  }

  onSearchChange(): void {
    this.searchChange.emit(this.searchTerm);
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.searchChange.emit('');
  }
}
