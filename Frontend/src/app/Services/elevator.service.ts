import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, interval, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ElevatorService {
  private apiUrl = 'http://localhost:5272/Elevator';

  constructor(private http: HttpClient) { }

  getStatus(): Observable<any> {
    return this.http.get(`${this.apiUrl}/status`);
  }

  callElevator(floor: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/call`, floor);
  }

  pollStatus(intervalTime: number = 1000): Observable<any> {
    return interval(intervalTime).pipe(
      switchMap(() => this.getStatus())
    );
  }
}
