import { Component, OnInit } from "@angular/core";

import { DataOptionsService } from '../shared/data-options.service';
import { MigrationsOptions } from '../../models/domain/DTO/migrations-options.model';

@Component({
    templateUrl: './home.component.html',
    providers: [DataOptionsService]
})
export class HomeComponent implements OnInit {
    constructor(
        private _dataOptionsService: DataOptionsService) {
    }

    get model(): MigrationsOptions {
        return this._dataOptionsService.migrationsOptions;
    }

    get infoMessageVisible(): boolean {
        if (this._dataOptionsService.infoMessage == undefined
            || this._dataOptionsService.infoMessage == null
            || this._dataOptionsService.infoMessage.trim() == "") {
            return false;
        }
        return true;
    }

    get infoMessage(): string {
        return this._dataOptionsService.infoMessage;
    }

    get contextName(): string {
        return this._dataOptionsService.contextName;
    }

    get migrationName(): string {
        return this._dataOptionsService.migrationName;
    }

    ngOnInit() {
        this._dataOptionsService.getDbservices();
    }

    onChangeContext(context: string): void {
        this._dataOptionsService.contextName = context;
    }

    onChangeMigration(migration: string): void {
        this._dataOptionsService.migrationName = migration;
    }

    onSeedDatabase(): void {
        this._dataOptionsService.seedDatabase(false);
    }

    onSeedDatabaseFromFile(): void {
        this._dataOptionsService.seedDatabase(true);
    }

    onApplyMigrations(): void {
        this._dataOptionsService.applyMigrations();
    }

    onClearDatabase(): void {
        this._dataOptionsService.clearDatabase();
    }

    onSaveData(): void {
        this._dataOptionsService.saveData();
    }

    onChooseContext(): void {
        this._dataOptionsService.chooseContext();
    }
}
