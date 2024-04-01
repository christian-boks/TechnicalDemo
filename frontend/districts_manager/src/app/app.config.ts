import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { errorInterceptorProvider } from './utils/errorhandler';
import { BASE_PATH } from './generated/variables';


export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(HttpClientModule),
    errorInterceptorProvider,
    { provide: BASE_PATH, useValue: 'http://localhost:5278' }
  ]
};
