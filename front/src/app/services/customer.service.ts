import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

import { Customer } from '../models/Customer';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseURL = `${environment.mainUrlAPI}customer`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseURL);
  }

  getById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseURL}/${id}`);
  }

  post(customer: Customer) {
    return this.http.post(this.baseURL, customer);
  }

  put(customer: Customer) {
    return this.http.put(`${this.baseURL}/${customer.id}`, customer);
  }

  delete(id: string) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
