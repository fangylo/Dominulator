﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class DefaultPlayerAction
        : IPlayerAction
    {        

        private Type NoCard()
        {
            return null;
        }

        private void NoDefaultAction()
        {

        }

        private Type PlayerMustMakeCardChoice()
        {
            throw new NotImplementedException();
        }

        private bool PlayerMustMakeChoice()
        {
            throw new NotImplementedException();
        }

        private PlayerActionChoice PlayerMustMakeActionChoice()
        {
            throw new NotImplementedException();
        }

        private int PlayerMustChooseNumber()
        {
            throw new NotImplementedException();
        }

        private Type NoCardIfOptional(bool isOptional)
        {
            if (isOptional)
            {
                return NoCard();
            }

            return PlayerMustMakeCardChoice();
        }

        virtual public string PlayerName 
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        virtual public void BeginTurn()
        {
            NoDefaultAction();
        }

        virtual public void EndTurn()
        {
            NoDefaultAction();
        }

        virtual public Type BanCardForCurrentPlayerRevealedCards(GameState gameState)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type BanCardForCurrentPlayerPurchase(GameState gameState)
        {            
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetActionFromHandToPlay(GameState gameState, bool isOptional)
        {
            return NoCardIfOptional(isOptional);
        }

        virtual public Type GetTreasureFromHandToPlay(GameState gameState)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromSupplyToBuy(GameState gameState, CardPredicate cardPredicate)
        {
            return NoCard();
        }

        virtual public Type GuessCardTopOfDeck(GameState gameState)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromSupplyToGain(GameState gameState, CardPredicate acceptableCard, bool isOptional)
        {
            return NoCardIfOptional(isOptional);   
        }

        virtual public Type GetCardFromRevealedCarsToTopDeck(BagOfCards revealedCards)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromRevealedCardsToTrash(PlayerState player, BagOfCards revealedCards, CardPredicate acceptableCard)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromHandToTopDeck(GameState gameState, CardPredicate acceptableCard)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromHandToPassLeft(GameState gameState)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromHandToDiscard(GameState gameState, bool isOptional)
        {
            return NoCardIfOptional(isOptional);
        }

        virtual public Type GetCardFromHandToTrash(GameState gameState, CardPredicate acceptableCard, bool isOptional)
        {
            return PlayerMustMakeCardChoice();
        }

        virtual public Type GetCardFromRevealedCardsToPutOnDeck(GameState gameState)
        {
            return PlayerMustMakeCardChoice();
        }        

        virtual public bool ShouldPlayerDiscardCardFromDeck(GameState gameState, PlayerState player, Card card)
        {
            return PlayerMustMakeChoice();
        }

        virtual public bool ShouldPutCardInHand(GameState gameState, Card card)
        {
            return PlayerMustMakeChoice();
        }

        virtual public bool WantToResign(GameState gameState)
        {
            return false;
        }        

        virtual public bool ShouldPutDeckInDiscard(GameState gameState)
        {
            return PlayerMustMakeChoice();
        }

        virtual public bool ShouldRevealCard(GameState gameState, Card card)
        {
            return PlayerMustMakeChoice();
        }

        virtual public bool ShouldTrashCard(GameState gameState, Card card)
        {
            return PlayerMustMakeChoice();
        }

        virtual public bool ShouldGainCard(GameState gameState, Card card)
        {
            return PlayerMustMakeChoice();
        }

        virtual public PlayerActionChoice ChooseAction(GameState gameState, IsValidChoice acceptableChoice)
        {
            return PlayerMustMakeActionChoice();
        }

        virtual public int GetNumberOfCardsFromDiscardToPutInHand(GameState gameState, int maxNumber)
        {
            return PlayerMustChooseNumber();
        }
    }    
}
