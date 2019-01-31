import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'asterik',
    template: `
        <span style="font-weight:bold; color:red">*</span>
`
})

export class RequiredAsterikComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
}