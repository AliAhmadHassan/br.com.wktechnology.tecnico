import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from 'src/app/models/category.model';
import { ResponseDTO } from 'src/app/models/responsedto.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  post(category: Category){
    return this.http.post<ResponseDTO>(`${environment.baseApiUrl}/category`, category);
  }

  delete(id: number){
    return this.http.delete(`${environment.baseApiUrl}/category/${id}`);
  }

  put(category: Category){
    return this.http.put<ResponseDTO>(`${environment.baseApiUrl}/category`, category);
  }

  getById(id: number){
    return this.http.get<ResponseDTO>(`${environment.baseApiUrl}/category/${id}`);
  }

  get(){
    return this.http.get<ResponseDTO>(`${environment.baseApiUrl}/category`);
  }
}
