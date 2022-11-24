import { ResponseDTO } from './../../models/responsedto.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  post(product: Product){
    return this.http.post<ResponseDTO>(`${environment.baseApiUrl}/product`, product);
  }

  delete(id: number){
    return this.http.delete(`${environment.baseApiUrl}/product/${id}`);
  }

  put(product: Product){
    return this.http.put<ResponseDTO>(`${environment.baseApiUrl}/product`, product);
  }

  getById(id: number){
    return this.http.get<ResponseDTO>(`${environment.baseApiUrl}/product/${id}`);
  }

  getByIdCategory(idcategory: number){
    return this.http.get<ResponseDTO>(`${environment.baseApiUrl}/product/${idcategory}/idcategory`);
  }

  get(){
    return this.http.get<ResponseDTO>(`${environment.baseApiUrl}/product`);
  }
}
