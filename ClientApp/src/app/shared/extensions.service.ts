import { HttpHeaders } from '@angular/common/http'; 
import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root',
  })
  export class ExtensionsService {
	private _fullAuthenticationHeaders: object | undefined
  
	constructor() {
		const token = sessionStorage.getItem('token') as string
		const headerObj = new HttpHeaders({
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${token}`,
		});

		const httpFullOptions = {
		  headers: headerObj
		};
	
		this._fullAuthenticationHeaders = httpFullOptions;
	}

	get authenticationHeadersFull(): object | undefined {
		return this._fullAuthenticationHeaders;
	}

}