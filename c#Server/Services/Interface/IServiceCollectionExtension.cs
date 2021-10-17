using HebrewNLP;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services.Classes;
using System;
using System.Collections.Generic;
using System.Text;


namespace Services.Interface
{
    public static class IServiceCollectionExtension
    {
         public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IwordTimeManager, wordTimeManager>();
            services.AddScoped<IWordsTimeRepository, WordsTimeRepository>();
            services.AddScoped<IWordsToSubjectsRepository, WordsToSubjectsRepository>();
            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddScoped<ISubjectsRepository, SubjectsRepository>();
            services.AddScoped<IBaseEventRepository, BaseEventRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBaseEventRepository, BaseEventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ILatLngService, LatLngService>();
            services.AddScoped<ICategoryEventRepository, CategoryEventRepository>();
            services.AddScoped<ICategoryMedicinesRepository, CategoryMedicinesRepository>();
            services.AddScoped<ICategoryMeetingsRepository, CategoryMeetingsRepository>();
            services.AddScoped<ICategoryPreparationRepository, CategoryPreparationRepository>();
            services.AddScoped<ICategoryCommunicationRepository, CategoryCommunicationRepository>();
            services.AddScoped<ICategoryShoppingRepository, CategoryShoppingRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddRepositories();
            return services;
        }
    }
}
