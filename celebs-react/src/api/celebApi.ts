import type { Celeb } from '../models/celeb';
import config from '../config';

const BASE_URL = config.apiBaseUrl;

export async function getAllCelebs(
  sortBy: string | null 
): Promise<Celeb[]> {
  if (!sortBy) {
   sortBy=config.defaultSortBy;
  }
  if (sortBy !== 'name' && sortBy !== 'gender' && sortBy !== 'date') {
    throw new Error(`Invalid sortBy value: ${sortBy}`); 
  }
  const params = new URLSearchParams();
  params.append('sortByGender', String(sortBy === 'gender'));
  params.append('sortByName', String(sortBy === 'name'));
  params.append('sortByDate', String(sortBy === 'date'));
  const res = await fetch(`${BASE_URL}/v1/celebs?${params.toString()}`);
  if (!res.ok) {
    throw new Error(`Failed to fetch celebs: ${res.status}`);
  }
  return res.json();
}

export async function deleteCeleb(id: string): Promise<void> {
  const res = await fetch(`${BASE_URL}/v1/celeb/${id}`, {
    method: "DELETE"
  });

  if (!res.ok) {
    throw new Error(`Failed to delete celeb: ${res.status}`);
  }
}

export async function resetCelebs(): Promise<void> {
  const res = await fetch(`${BASE_URL}/v1/reset`, {
    method: "POST"
  });

  if (!res.ok) {
    throw new Error(`Failed to reset the celebs list: ${res.status}`);
  }
}


