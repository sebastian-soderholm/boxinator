import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/shared/session.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.page.html',
  styleUrls: ['./settings.page.scss']
})
export class SettingsPage implements OnInit {
  panelOpenState = false;
  constructor(
    private readonly sessionService: SessionService,
  ) {}

  ngOnInit(): void {
    this.onChanges();
  }

  onChanges() {
  }
}
