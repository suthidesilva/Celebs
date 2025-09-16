import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Celeb } from '../../models/celeb';
import { CelebService } from '../../services/celeb.service';
import { appConstants } from '../../config/app.constants';
import { ConfirmationDialogComponent } from '../../components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-celeb-details',
  standalone: true,
  imports: [CommonModule, FormsModule, ConfirmationDialogComponent],
  templateUrl: './celeb-details.component.html',
  styleUrls: ['./celeb-details.component.css']
})
export class CelebDetailsComponent implements OnInit, OnDestroy {
  celeb: Celeb | null = null;
  originalCeleb: Celeb | null = null;
  loading = true;
  editing = false;
  saving = false;
  showSaveConfirmation = false;
  showResetConfirmation = false;
  appConstants = appConstants;
  
  private destroy$ = new Subject<void>();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private celebService: CelebService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadCeleb(id);
    } else {
      this.router.navigate(['/']);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadCeleb(id: string): void {
    this.loading = true;
    this.celebService.getCelebById(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (celeb) => {
          this.celeb = { ...celeb };
          this.originalCeleb = { ...celeb };
          this.loading = false;
        },
        error: (err) => {
          console.error('Failed to load celeb:', err);
          this.toastr.error('Failed to load celebrity details');
          this.loading = false;
          this.router.navigate(['/']);
        }
      });
  }

  toggleEdit(): void {
    this.editing = !this.editing;
    if (!this.editing) {
      // Reset to original data when canceling edit
      this.celeb = this.originalCeleb ? { ...this.originalCeleb } : null;
    }
  }

  addRole(): void {
    if (this.celeb) {
      this.celeb.roles = [...(this.celeb.roles || []), ''];
    }
  }

  removeRole(index: number): void {
    if (this.celeb && this.celeb.roles) {
      this.celeb.roles.splice(index, 1);
    }
  }

  trackByIndex(index: number): number {
    return index;
  }

  save(): void {
    this.showSaveConfirmation = true;
  }

  confirmSave(): void {
    if (!this.celeb) return;

    // Validate required fields
    if (!this.celeb.name?.trim()) {
      this.toastr.error('Name is required');
      this.showSaveConfirmation = false;
      return;
    }

    if (!this.celeb.gender?.trim()) {
      this.toastr.error('Gender is required');
      this.showSaveConfirmation = false;
      return;
    }

    // Filter out empty roles
    if (this.celeb.roles) {
      this.celeb.roles = this.celeb.roles.filter(role => role.trim() !== '');
    }

    this.saving = true;
    this.showSaveConfirmation = false;
    
    this.celebService.updateCeleb(this.celeb.id, this.celeb)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (updatedCeleb) => {
          this.celeb = updatedCeleb;
          this.originalCeleb = { ...updatedCeleb };
          this.editing = false;
          this.saving = false;
          this.toastr.success('Celebrity updated successfully!');
        },
        error: (err) => {
          console.error('Failed to update celeb:', err);
          this.toastr.error('Failed to update celebrity');
          this.saving = false;
        }
      });
  }

  cancelSave(): void {
    this.showSaveConfirmation = false;
  }

  reset(): void {
    this.showResetConfirmation = true;
  }

  confirmReset(): void {
    if (this.originalCeleb) {
      this.celeb = { ...this.originalCeleb };
      this.editing = false;
      this.showResetConfirmation = false;
      this.toastr.info('Changes discarded');
    }
  }

  cancelReset(): void {
    this.showResetConfirmation = false;
  }

  goBack(): void {
    this.router.navigate(['/']);
  }
}
