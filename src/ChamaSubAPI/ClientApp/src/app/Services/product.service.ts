import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private readonly compositeProducts = '/api/compositeproducts';
  private readonly singleProducts = '/api/productAttributes';
  private readonly categories = '/api/categories';
  private readonly productNames = '/api/products';

  constructor(private http: HttpClient) { }

  getCategoryItems() {
    return this.http.get<any>(this.categories);
  }

  createCategory(category) {
    return this.http.post(this.categories, category)
  }

  

  getOptions() {
    return this.http.get<any>('/api/options');
  }

  getOptionValues() {
    return this.http.get<any>('/api/optionValues');
  }

  getProductNames(productName) {
    return this.http.get<any>(this.productNames);
  }

  createProductName(productName) {
    return this.http.post(this.productNames, productName);
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
