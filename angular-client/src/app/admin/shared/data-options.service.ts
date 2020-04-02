import { Injectable } from "@angular/core";

import { MigrationsOptions } from '../../models/domain/DTO/migrations-options.model';
import { Url } from '../../models/url.model';
import { RestDatasource } from '../../core/rest-datasource.service';

@Injectable()
export class DataOptionsService {
    constructor(
        private _rest: RestDatasource) { }

    migrationsOptions: MigrationsOptions = new MigrationsOptions();
    contextName: string = "";
    migrationName: string = "";
    infoMessage: string = "";

    getDbservices(): void {
        this._rest.getAll<MigrationsOptions>(Url.dataOptions)
            .subscribe(result => this.processDataOptionsResult(result, true));
    }

    applyMigrations(): void {
        this._rest.getAll<MigrationsOptions>(`${Url.options_apply}/${this.contextName}/${this.migrationName}`)
            .subscribe(result => this.processDataOptionsResult(result, true));
    }

    seedDatabase(fromFile: boolean): void {
        let url: string = `${Url.options_seed}/${this.contextName}/${fromFile}`;
        this._rest.getAll<string>(url)
            .subscribe(result => this.infoMessage = result);
    }

    saveData(): void {
        this._rest.getAll<string>(Url.options_save)
            .subscribe(result => this.infoMessage = result);
    }

    clearDatabase(): void {
        this._rest.getAll<string>(`${Url.options_clear}/${this.contextName}`)
            .subscribe(result => this.infoMessage = result);
    }

    chooseContext(): void {
        this._rest.getAll<MigrationsOptions>(`${Url.options_context}/${this.contextName}`)
            .subscribe(result => this.processDataOptionsResult(result, false));
    }

    private processDataOptionsResult(result: MigrationsOptions, initializeContext: boolean): void {
        this.migrationsOptions = result;

        if (initializeContext) {
            this.contextName = this.migrationsOptions.contextNames[0];
        }

        let index: number = this.migrationsOptions.allMigrations.length - 1;
        this.migrationName = this.migrationsOptions.allMigrations[index];
        this.infoMessage = this.migrationsOptions.infoMessage;
    }
}
