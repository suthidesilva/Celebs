import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Celeb } from '../../models/celeb';
import { CelebService } from '../../services/celeb.service';
import { CelebCardComponent } from '../../components/celeb-card/celeb-card.component';
import { CelebToolbarComponent } from '../../components/celeb-toolbar/celeb-toolbar.component';
import { appConstants } from '../../config/app.constants';

@Component({
  selector: 'app-list-celebs',
  standalone: true,
  imports: [CommonModule, DragDropModule, CelebCardComponent, CelebToolbarComponent],
  templateUrl: './list-celebs.component.html',
  styleUrls: ['./list-celebs.component.css']
})
export class ListCelebsComponent implements OnInit, OnDestroy {
  celebs: Celeb[] = [];
  loading = true;
  viewMode: 'grid' | 'list' = appConstants.defaultViewMode as 'grid' | 'list';
  sortBy: 'name' | 'gender' | 'date' = appConstants.defaultSortBy as 'name' | 'gender' | 'date';
  appConstants = appConstants;
  
  private destroy$ = new Subject<void>();

  constructor(
    private celebService: CelebService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.fetchCelebs();
    document.title = appConstants.appName;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  fetchCelebs(): void {
    this.loading = true;
    this.celebService.getAllCelebs(this.sortBy)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (celebs) => {
          this.celebs = celebs;
          this.loading = false;
        },
        error: (err) => {
          console.error('Failed to load celebs:', err);
          this.loading = false;
        }
      });
  }

  handleReset(): void {
    this.celebService.resetCelebs()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this.fetchCelebs();
          this.toastr.success('Celebs list has been reset');
        },
        error: (err) => {
          console.error('Failed to reset celebs:', err);
          this.toastr.error('Failed to reset celebs list');
        }
      });
  }

  toggleViewMode(): void {
    this.viewMode = this.viewMode === 'grid' ? 'list' : 'grid';
  }

  onDeleteCeleb(celebId: string): void {
    this.celebService.deleteCeleb(celebId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          const celeb = this.celebs.find(c => c.id === celebId);
          this.toastr.success(`${celeb?.name} deleted.`);
          this.fetchCelebs();
        },
        error: (err) => {
          console.error('Failed to delete celeb:', err);
          this.toastr.error('Failed to delete celeb.');
        }
      });
  }

  onDragDrop(event: CdkDragDrop<Celeb[]>): void {
    if (appConstants.enableDragAndDrop) {
      moveItemInArray(this.celebs, event.previousIndex, event.currentIndex);
    }
  }

  getCurrentYear(): number {
    return new Date().getFullYear();
  }
}
