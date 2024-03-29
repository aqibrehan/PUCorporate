﻿namespace PUCorporate.DataAccessLayer.Services.Interfaces
{
    public interface IGeneric
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);

        Task SaveData<T>(string SP, T parameters);
    }
}
