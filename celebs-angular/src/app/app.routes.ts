import { Routes } from '@angular/router';
import { ListCelebsComponent } from './pages/list-celebs/list-celebs.component';
import { CelebDetailsComponent } from './pages/celeb-details/celeb-details.component';

export const routes: Routes = [
  { path: '', component: ListCelebsComponent },
  { path: 'celeb/:id', component: CelebDetailsComponent },
  { path: '**', redirectTo: '' }
];
