import { Component } from '@angular/core';
import { DistrictsComponent } from './components/districts/districts.component';
import { ApiModule } from './generated/api.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [DistrictsComponent, ApiModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Districts Manager';
}

export const BACKEND_URL = "http://localhost:5278";