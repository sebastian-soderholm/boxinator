import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExtensionsService } from 'src/app/shared/extensions.service';
import { SessionService } from 'src/app/shared/session.service';
import { environment } from 'src/environments/environment';

const apiUrl = environment.baseURL;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private readonly http: HttpClient,
    private readonly sessionService: SessionService,
    private readonly extensionService: ExtensionsService) {
  }

  //delete shipment
  public deleteUser(userId: number) {
    return this.http.delete<boolean>(apiUrl + '/account/' + userId, this.extensionService.authenticationHeadersFull)
  }
}
