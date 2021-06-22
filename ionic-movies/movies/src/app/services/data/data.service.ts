import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  //https://www.codegrepper.com/code-examples/typescript/passing+data+from+one+page+to+another+in+ionic+4

  //https://ionicacademy.com/pass-data-angular-router-ionic-4/
  //2. Service and Resolve Function

  private data = [];

  constructor() {}

  setData(id, data) {
    this.data[id] = data;
  }

  getData(id) {
    return this.data[id];
  }
}
