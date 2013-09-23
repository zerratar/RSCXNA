using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSCXNALib;

namespace RSCXNA
{
    public static class MudclientActionExtensions
    {
        #region displaying and drawing text
        public static void DisplayMessage(this mudclient mc, string message)
        {
            mc.displayMessage(message);
        }

        public static void DisplayMessage(this mudclient mc, string message, int type)
        {
            mc.displayMessage(message, type);
        }
        #endregion

        #region Boxes, such as Bank, Trade, Duel, etc.
        public static bool IsTradeWindowVisible(this mudclient mc, TradeAndDuelState state)
        {
            return state == TradeAndDuelState.Initial ? mc.showTradeBox : mc.showTradeConfirmBox;
        }
        public static void AcceptTrade(this mudclient mc, TradeAndDuelState state)
        {
            if (state == TradeAndDuelState.Initial) 
            { 
            }
            else 
            { 
            }
        }
        public static void DeclineTrade(this mudclient mc)
        {

        }

        public static bool IsDuelWindowVisible(this mudclient mc, TradeAndDuelState state)
        {
            return state == TradeAndDuelState.Initial ? mc.showDuelBox : mc.showDuelConfirmBox;
        }
        public static void AcceptDuel(this mudclient mc, TradeAndDuelState state)
        {
            if (state == TradeAndDuelState.Confirm)
            {
                mc.duelConfirmOurAccepted = true;
                mc.streamClass.createPacket(87);
                mc.streamClass.formatPacket();
            }
        }
        public static void DeclineDuel(this mudclient mc)
        {
            mc.showDuelConfirmBox = false;
            mc.streamClass.createPacket(35);
            mc.streamClass.formatPacket();
        }

        #endregion
    }

    public enum TradeAndDuelState
    {
        Initial, Confirm
    }
}
