import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class ProductService {
  //Product APIs
  private readonly compositeProducts = '/api/compositeproducts';
  private readonly singleProducts = '/api/productAttributes';

  // Related APIs
  private readonly categories = '/api/categories';
  private readonly productNames = '/api/products';
  private readonly options = '/api/options';
  private readonly optionValues = '/api/optionValues';
  private readonly skuValues = '/api/skuvalues';
  private readonly prices = '/api/productAttributes';

  constructor(private http: HttpClient) { }

  getCategoryItems() {
    return this.http.get<any>(this.categories);
  }

  createCategory(category) {
    return this.http.post(this.categories, category)
  }

  

  getOptions() {
    return this.http.get<any>(this.options);
  }
  createOptions(option) {
    return this.http.post(this.options, option)
  }

  getOptionValues() {
    return this.http.get<any>(this.optionValues);
  }

  createOptionValues(optionValue) {
    return this.http.post(this.optionValues, optionValue)
  }

  getProductNames() {
    return this.http.get<any>(this.productNames);
  }

  createProductName(productName) {
    return this.http.post(this.productNames, productName);
  }


  getSkuValues() {
    return this.http.get<any>(this.skuValues);

  }

  createSkuValues(skuValue) {
    return this.http.post(this.skuValues, skuValue);

  }

  getSkuAttributes() {
    return this.http.get<any>(this.prices);
  }

  createSkuAttributes(price) {
    return this.http.get<any>(this.prices, price);
  }

  getSingleProducts() {
    return this.http.get<any>(this.singleProducts);
  }

  getCompositeProducts() {
    return this.http.get<any>(this.compositeProducts);
  }
}
