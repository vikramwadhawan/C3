import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'loader',
    templateUrl: './loader.component.html'
})

export class LoaderComponent implements OnInit {

    @Input('loaderMessage') _loaderMessage: string;
    constructor() { }

    ngOnInit() { }
}