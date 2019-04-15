using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShortRent.Data;
using ShortRent.Core.Data;
using ShortRent.Core.Domain;
using System.Threading.Tasks;
using ShortRent.Core.Cache;
using ShortRent.Core.Config;
using ShortRent.Core.Log;

namespace ShortRent.Service
{
    public class HistoryOperatorService:IHistoryOperatorService
    {
        #region Fields
        private readonly IRepository<HistoryOperator> _historyOperatorRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly ApplicationConfig _config;
        private const string historyOperatorServiceCache = nameof(HistoryOperatorService) + nameof(HistoryOperator);
        #endregion

        #region Construction
        public HistoryOperatorService(IRepository<HistoryOperator> repository, ICacheManager cacheManager, ILogger logger, ApplicationConfig config)
        {
            this._historyOperatorRepository = repository;
            this._cacheManager = cacheManager;
            this._logger = logger;
            this._config = config;

        }
        #endregion

        #region Methods
        /// <summary>
        /// 创建历史记录
        /// </summary>
        /// <param name="model"></param>
        public void CreateHistoryOperator(HistoryOperator model)
        {
            try
            {
                _historyOperatorRepository.Insert(model);
                _cacheManager.Remove(historyOperatorServiceCache);
            }
            catch(Exception e)
            {
                _logger.Debug("创建历史记录",e);
                throw e;
            }
        }
        #endregion
    }
}
