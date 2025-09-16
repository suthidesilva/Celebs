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

  getAllCelebs(sortBy: string | null, search?: string): Observable<Celeb[]> {
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
    
    if (search && search.trim()) {
      params.append('search', search.trim());
    }
    
    return this.http.get<Celeb[]>(`${this.baseUrl}/v1/celebs?${params.toString()}`);
  }

  getCelebById(id: string): Observable<Celeb> {
    return this.http.get<Celeb>(`${this.baseUrl}/v1/${id}`);
  }

  updateCeleb(id: string, celeb: Partial<Celeb>): Observable<Celeb> {
    return this.http.put<Celeb>(`${this.baseUrl}/v1/celeb/${id}`, celeb);
  }

  deleteCeleb(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/v1/celeb/${id}`);
  }

  resetCelebs(): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/v1/reset`, {});
  }
}
