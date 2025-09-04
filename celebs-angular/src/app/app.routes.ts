import { Routes } from '@angular/router';
import { ListCelebsComponent } from './pages/list-celebs/list-celebs.component';

export const routes: Routes = [
  { path: '', component: ListCelebsComponent },
  { path: '**', redirectTo: '' }
];
