﻿using System;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.Handlers.Command
{
    public class CategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CategoryEntity>,
        ICommandHandler<UpdateCategoryCommand, bool>,
        ICommandHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryCommandHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
        }

        public CategoryEntity Handle(CreateCategoryCommand command)
        {
            var category = command.MapCategoryEntity();
            return _categoriesRepository.AddCategory(category);
        }

        public bool Handle(UpdateCategoryCommand command)
        {
            var category = command.MapCategoryEntity();
            return _categoriesRepository.UpdateCategory(category);
        }

        public bool Handle(DeleteCategoryCommand command)
        {
            var category = command.MapCategoryEntity();
            var hasChildrenBeenDeleted = false;
            if (category.ParentId == null)
            {
                hasChildrenBeenDeleted = _categoriesRepository.DeleteChildrenCategories(category.Id);
            }

            return hasChildrenBeenDeleted || _categoriesRepository.DeleteCategory(category);
        }
    }
}