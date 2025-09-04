import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Celeb } from '../models/celeb';
import { appConstants } from '../config/app.constants';

@Injectable({
  providedIn: 'root'
})
export class CelebService {
  private baseUrl = appConstants.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getAllCelebs(sortBy: string | null): Observable<Celeb[]> {
    if (!sortBy) {
      sortBy = appConstants.defaultSortBy;
    }
    if (sortBy !== 'name' && sortBy !== 'gender' && sortBy !== 'date') {
      throw new Error(`Invalid sortBy value: ${sortBy}`);
    }
    
    const params = new URLSearchParams();
    params.append('sortByGender', String(sortBy === 'gender'));
    params.append('sortByName', String(sortBy === 'name'));
    params.append('sortByDate', String(sortBy === 'date'));
    
    return this.http.get<Celeb[]>(`${this.baseUrl}/v1/celebs?${params.toString()}`);
  }

  deleteCeleb(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/v1/celeb/${id}`);
  }

  resetCelebs(): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/v1/reset`, {});
  }
}
