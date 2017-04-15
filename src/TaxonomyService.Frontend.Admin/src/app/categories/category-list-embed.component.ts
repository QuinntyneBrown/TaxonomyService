import { Category } from "./category.model";

const template = require("./category-list-embed.component.html");
const styles = require("./category-list-embed.component.scss");

export class CategoryListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "categories"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.categories.length; i++) {
            let el = this._document.createElement(`ce-category-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.categories[i]));
            this.appendChild(el);
        }    
    }

    categories:Array<Category> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "categories":
                this.categories = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-category-list-embed", CategoryListEmbedComponent);
