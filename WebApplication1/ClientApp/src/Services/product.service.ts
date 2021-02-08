import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProductModel } from '../models/product.model';


export class ProductService {

  appUrl: string = "";

  private productList = new BehaviorSubject<ProductModel>(null);

  currentProductList = this.productList.asObservable();

  constructor(private httpObject: HttpClient, @Inject('BASE_URL')_baseUrl:string)
  {
    this.appUrl = _baseUrl;
  }

  getAllProducts(): Observable<ProductModel> {
    return this.httpObject.get<ProductModel>(this.appUrl + "api/Products");
  }

  getActiveProducts(): Observable<ProductModel> {
    return this.httpObject.get<ProductModel>(this.appUrl + "api/Products/ActiveProducts");
  }

  getDangerousDrugs(): Observable<ProductModel> {
    return this.httpObject.get<ProductModel>(this.appUrl + "api/Products/ActiveDangerousDrugs");
  }

  updateDescription(id: number, description: string) {
    return this.httpObject.put(this.appUrl + "api/Products/" + id + "/" + description, description);
  }

  changeProductList(productList: ProductModel) {
    this.productList.next(productList);
  }
}
