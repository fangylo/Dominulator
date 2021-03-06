﻿using Dominion;
using Dominion.Strategy;
using CardTypes = Dominion.CardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.GeneticAlgorithm;

namespace Program
{
    class CompareBigMoneyWithCardDescription
             : IScoreSpecies<BigMoneyWithCardDescription>
    {
        PlayerAction playerAction;

        public CompareBigMoneyWithCardDescription(PlayerAction playerAction)
        {
            this.playerAction = playerAction;
        }

        public double GetScore(BigMoneyWithCardDescription current)     
        {
            return current.GetScoreVs(this.playerAction);
        }            
    }  
}
