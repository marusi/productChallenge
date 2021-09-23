import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private readonly compositeProducts = '/api/compositeproducts';
  private readonly singleProducts = '/api/productAttributes';

  constructor(private http: HttpClient) { }

  getCategoryItems() {
    return this.http.get<any>('/api/categories');
  }
  getOptions() {
    return this.http.get<any>('/api/options');
  }

  getOptionValues() {
    return this.http.get<any>('/api/optionValues');
  }

  getProductNames() {
    return this.http.get<any>('/api/products');
  }


  getSkuValues() {
    return this.http.get<any>('/api/skuvalues');

  }

  getSingleProducts() {
    return this.http.get<any>(this.singleProducts);
  }

  getCompositeProducts() {
    return this.http.get<any>(this.compositeProducts);
  }
}
