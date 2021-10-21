import { HttpHeaders } from '@angular/common/http'; 
import { Injectable } from '@angular/core';
import { SessionService } from './session.service';
import { LoginService } from '../login/services/login.service';

@Injectable({
	providedIn: 'root',
  })
  export class ExtensionsService {
	private _fullAuthenticationHeaders: object | undefined
  
	constructor(
		private readonly sessionService: SessionService,
		private readonly loginService: LoginService
	) {
		const token = this.sessionService.token;
		//const token = this.sessionService.getToken();
		
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