import { MessageBannerComponent } from './message-banner.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { LoaderComponent } from './loader.component';
import { RequiredAsterikComponent } from './required-asterik.component';
import { NgModule } from '@angular/core';
import { LightBoxComponent } from './lightbox.component';


@NgModule({
    imports: [
      BrowserModule,
      FormsModule  
    ],
    exports: [
        LightBoxComponent,
        LoaderComponent,
        RequiredAsterikComponent,      
        MessageBannerComponent             
    ],
    declarations: [
        LightBoxComponent,
        LoaderComponent,
        RequiredAsterikComponent,      
        MessageBannerComponent,      
    ],
    providers: [],
})
export class SharedModule { }
