﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Bybit.Net.Objects.Models.V5;
using CryptoExchange.Net;
using Bybit.Net.Enums;
using Bybit.Net.Enums.V5;
using CryptoExchange.Net.Converters;
using System.Globalization;
using Bybit.Net.Interfaces.Clients.V5;

namespace Bybit.Net.Clients.V5
{
    /// <inheritdoc />
    public class BybitRestClientApiAccount : IBybitRestClientApiAccount
    {
        private BybitRestClientApi _baseClient;

        internal BybitRestClientApiAccount(BybitRestClientApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult> SetLeverageAsync(
            Category category,
            string symbol,
            decimal buyLeverage,
            decimal sellLeverage,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "symbol", symbol },
                { "buyLeverage", buyLeverage.ToString(CultureInfo.InvariantCulture) },
                { "sellLeverage", sellLeverage.ToString(CultureInfo.InvariantCulture) }
            };

            return await _baseClient.SendRequestAsync(_baseClient.GetUrl("v5/position/set-leverage"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Switch Cross Isolated Margin

        /// <inheritdoc />
        public async Task<WebCallResult> SwitchCrossIsolatedMarginAsync(
            Category category,
            string symbol,
            TradeMode tradeMode,
            decimal buyLeverage,
            decimal sellLeverage,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "symbol", symbol },
                { "tradeMode", EnumConverter.GetString(tradeMode) },
                { "buyLeverage", buyLeverage.ToString(CultureInfo.InvariantCulture) },
                { "sellLeverage", sellLeverage.ToString(CultureInfo.InvariantCulture) }
            };

            return await _baseClient.SendRequestAsync(_baseClient.GetUrl("v5/position/switch-isolated"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Set TakeProfit StopLoss Mode

        /// <inheritdoc />
        public async Task<WebCallResult<BybitTakeProfitStopLossMode>> SetTakeProfitStopLossModeAsync(
            Category category,
            string symbol,
            StopLossTakeProfitMode tpSlMode,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "symbol", symbol },
                { "tpSlMode", EnumConverter.GetString(tpSlMode) }
            };

            return await _baseClient.SendRequestAsync<BybitTakeProfitStopLossMode>(_baseClient.GetUrl("v5/position/set-tpsl-mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Switch Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SwitchPositionModeAsync(
            Category category,
            Enums.V5.PositionMode mode,
            string? symbol = null,
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "mode", EnumConverter.GetString(mode) },
            };

            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("coin", asset);

            return await _baseClient.SendRequestAsync(_baseClient.GetUrl("v5/position/switch-mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Set Risk Limit

        /// <inheritdoc />
        public async Task<WebCallResult<BybitSetRiskLimit>> SetRiskLimitAsync(
            Category category,
            string symbol,
            int riskId,
            PositionIdx? positionIdx = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "symbol", symbol },
                { "riskId", riskId }
            };

            parameters.AddOptionalParameter("positionIdx", EnumConverter.GetString(positionIdx));

            return await _baseClient.SendRequestAsync<BybitSetRiskLimit>(_baseClient.GetUrl("v5/position/set-risk-limit"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Set Auto Add Margin

        /// <inheritdoc />
        public async Task<WebCallResult> SetAutoAddMarginAsync(
            Category category,
            string symbol,
            bool autoAddMargin,
            PositionIdx? positionIdx = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "category", EnumConverter.GetString(category) },
                { "symbol", symbol },
                { "autoAddMargin", autoAddMargin ? "1" : "0" }
            };

            parameters.AddOptionalParameter("positionIdx", EnumConverter.GetString(positionIdx));

            return await _baseClient.SendRequestAsync(_baseClient.GetUrl("v5/position/set-auto-add-margin"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitBalance>>> GetBalancesAsync(
            AccountType accountType,
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", EnumConverter.GetString(accountType) }
            };

            parameters.AddOptionalParameter("coin", asset);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitBalance>>(_baseClient.GetUrl("v5/account/wallet-balance"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Borrow History

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitBorrowHistory>>> GetBorrowHistoryAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitBorrowHistory>>(_baseClient.GetUrl("v5/account/borrow-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Collateral Info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitCollateralInfo>>> GetCollateralInfoAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("currency", asset);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitCollateralInfo>>(_baseClient.GetUrl("v5/account/collateral-info"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Greeks

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitGreeks>>> GetAssetGreeksAsync(
            string? baseAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            parameters.AddOptionalParameter("baseCoin", baseAsset);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitGreeks>>(_baseClient.GetUrl("v5/asset/coin-greeks"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Fee Rate

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitFeeRate>>> GetFeeRateAsync(
            Category category,
            string? symbol = null,
            string? baseAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            if (category != Category.Undefined)
            {
                parameters.AddOptionalParameter("category", EnumConverter.GetString(category));
            }
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("baseCoin", baseAsset);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitFeeRate>>(_baseClient.GetUrl("v5/account/fee-rate"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitAccountInfo>> GetMarginAccountInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            return await _baseClient.SendRequestAsync<BybitAccountInfo>(_baseClient.GetUrl("v5/account/info"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitTransactionLog>>> GetTransactionHistoryAsync(
            AccountType? accountType = null,
            Category? category = null,
            string? asset = null,
            string? baseAsset = null,
            TransactionLogType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("accountType", EnumConverter.GetString(accountType));
            parameters.AddOptionalParameter("category", EnumConverter.GetString(category));
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("baseCoin", baseAsset);
            parameters.AddOptionalParameter("type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitTransactionLog>>(_baseClient.GetUrl("v5/account/transaction-log"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<WebCallResult<BybitSetMarginModeResult>> SetMarginModeAsync(
            MarginMode marginMode,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "setMarginMode", EnumConverter.GetString(marginMode) }
            };

            return await _baseClient.SendRequestAsync<BybitSetMarginModeResult>(_baseClient.GetUrl("v5/account/set-margin-mode"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitAccountAssetInfo>> GetAssetInfoAsync(
            AccountType accountType,
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", EnumConverter.GetString(accountType) }
            };
            parameters.AddOptionalParameter("coin", asset);

            var result = await _baseClient.SendRequestAsync<BybitAssetInfoWrapper>(_baseClient.GetUrl("v5/asset/transfer/query-asset-info"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return result.As<BybitAccountAssetInfo>(null);

            return result.As(result.Data.Spot);
        }

        #endregion

        #region Get All Asset Balances

        /// <inheritdoc />
        public async Task<WebCallResult<BybitAllAssetBalances>> GetAllAssetBalancesAsync(
            AccountType accountType,
            string? memberId = null,
            string? asset = null,
            bool? withBonus = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", EnumConverter.GetString(accountType) }
            };
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("memberId", memberId);
            parameters.AddOptionalParameter("withBonus", withBonus == true ? "1" : "0");

            return await _baseClient.SendRequestAsync<BybitAllAssetBalances>(_baseClient.GetUrl("v5/asset/transfer/query-account-coins-balance"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Balance

        /// <inheritdoc />
        public async Task<WebCallResult<BybitSingleAssetBalance>> GetAssetBalanceAsync(
            AccountType accountType,
            string asset,
            string? memberId = null,
            bool? withBonus = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", EnumConverter.GetString(accountType) },
                { "coin", asset }
            };
            parameters.AddOptionalParameter("memberId", memberId);
            parameters.AddOptionalParameter("withBonus", withBonus == true ? "1" : "0");

            return await _baseClient.SendRequestAsync<BybitSingleAssetBalance>(_baseClient.GetUrl("v5/asset/transfer/query-account-coin-balance"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Transferable Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<string>>> GetTransferableAssetsAsync(
            AccountType fromAccountType,
            AccountType toAccountType,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "fromAccountType", EnumConverter.GetString(fromAccountType) },
                { "toAccountType", EnumConverter.GetString(toAccountType) },
            };

            return await _baseClient.SendRequestAsync<BybitResponse<string>>(_baseClient.GetUrl("v5/asset/transfer/query-transfer-coin-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Create Internal transfer

        /// <inheritdoc />
        public async Task<WebCallResult<BybitTransferId>> CreateInternalTransferAsync(
            string asset,
            decimal quantity,
            AccountType fromAccountType,
            AccountType toAccountType,
            string? transferId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "fromAccountType", EnumConverter.GetString(fromAccountType) },
                { "toAccountType", EnumConverter.GetString(toAccountType) },
                { "coin", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "transferId", transferId ?? Guid.NewGuid().ToString() }
            };

            return await _baseClient.SendRequestAsync<BybitTransferId>(_baseClient.GetUrl("v5/asset/transfer/inter-transfer"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Internal Transfers

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitTransfer>>> GetInternalTransfersAsync(
            string? transferId = null,
            string? asset = null,
            TransferStatus? transferStatus = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("transferId", transferId);
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("status", EnumConverter.GetString(transferStatus));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitTransfer>>(_baseClient.GetUrl("v5/asset/transfer/query-inter-transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Create Universal transfer

        /// <inheritdoc />
        public async Task<WebCallResult<BybitTransferId>> CreateUniversalTransferAsync(
            string asset,
            decimal quantity,
            string fromMemberId,
            string toMemberId,
            AccountType fromAccountType,
            AccountType toAccountType,
            string? transferId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "fromAccountType", EnumConverter.GetString(fromAccountType) },
                { "toAccountType", EnumConverter.GetString(toAccountType) },
                { "coin", asset },
                { "fromMemberId", fromMemberId },
                { "toMemberId", toMemberId },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "transferId", transferId ?? Guid.NewGuid().ToString() }
            };

            return await _baseClient.SendRequestAsync<BybitTransferId>(_baseClient.GetUrl("v5/asset/transfer/universal-transfer"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Universal Transfers

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitTransfer>>> GetUniversalTransfersAsync(
            string? transferId = null,
            string? asset = null,
            TransferStatus? transferStatus = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("transferId", transferId);
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("status", EnumConverter.GetString(transferStatus));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitTransfer>>(_baseClient.GetUrl("v5/asset/transfer/query-universal-transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Allowed Deposit Info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitAllowedDepositInfoResponse>> GetAllowedDepositAssetInfoAsync(
            string? asset = null,
            string? network = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitAllowedDepositInfoResponse>(_baseClient.GetUrl("v5/asset/deposit/query-allowed-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Set Deposit Account

        /// <inheritdoc />
        public async Task<WebCallResult<BybitOperationResult>> SetDepositAccountAsync(
            AccountType accountType,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "accountType", EnumConverter.GetString(accountType) }
            };

            return await _baseClient.SendRequestAsync<BybitOperationResult>(_baseClient.GetUrl("v5/asset/deposit/deposit-to-account"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<WebCallResult<BybitDeposits>> GetDepositsAsync(
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitDeposits>(_baseClient.GetUrl("v5/asset/deposit/query-record"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<BybitDepositAddress>> GetDepositAddressAsync(
            string asset,
            string? networkType = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "coin", asset }
            };
            parameters.AddOptionalParameter("chainType", networkType);

            return await _baseClient.SendRequestAsync<BybitDepositAddress>(_baseClient.GetUrl("v5/asset/deposit/query-address"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitUserAssetInfos>> GetAssetInfoAsync(
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);

            return await _baseClient.SendRequestAsync<BybitUserAssetInfos>(_baseClient.GetUrl("v5/asset/coin/query-info"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawals

        /// <inheritdoc />
        public async Task<WebCallResult<BybitResponse<BybitWithdrawal>>> GetWithdrawalsAsync(
            string? withdrawId = null,
            string? asset = null,
            WithdrawalType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("withdrawID", withdrawId);
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("withdrawType", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("cursor", cursor);

            return await _baseClient.SendRequestAsync<BybitResponse<BybitWithdrawal>>(_baseClient.GetUrl("v5/asset/withdraw/query-record"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Delayed Withdrawal Quantity

        /// <inheritdoc />
        public async Task<WebCallResult<BybitDelayedWithdrawal>> GetDelayedWithdrawQuantityAsync(
            string asset,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "coin", asset }
            };

            return await _baseClient.SendRequestAsync<BybitDelayedWithdrawal>(_baseClient.GetUrl("v5/asset/withdraw/withdrawable-amount"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<BybitId>> WithdrawAsync(
            string asset,
            string network,
            string toAddress,
            decimal quantity,
            string? tag = null,
            bool? forceNetwork = null,
            AccountType? accountType = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "coin", asset },
                { "chain", network },
                { "address", toAddress },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) }
            };

            parameters.AddOptionalParameter("tag", tag);
            parameters.AddOptionalParameter("accountType", EnumConverter.GetString(accountType));
            parameters.AddOptionalParameter("forceChain", forceNetwork == null ? null : forceNetwork.Value ? 1 : 0);

            return await _baseClient.SendRequestAsync<BybitId>(_baseClient.GetUrl("v5/asset/withdraw/create"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<BybitOperationResult>> CancelWithdrawalAsync(
            string id,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "id", id },
            };

            return await _baseClient.SendRequestAsync<BybitOperationResult>(_baseClient.GetUrl("v5/asset/withdraw/cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Api Key Info

        /// <inheritdoc />
        public async Task<WebCallResult<BybitApiKeyInfo>> GetApiKeyInfoAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestAsync<BybitApiKeyInfo>(_baseClient.GetUrl("v5/user/query-api"), HttpMethod.Get, ct, null, true).ConfigureAwait(false);
        }

        #endregion
    }
}
