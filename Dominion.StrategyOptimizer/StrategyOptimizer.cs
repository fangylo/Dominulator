﻿using System;
using System.Linq;

using CardTypes = Dominion.CardTypes;
using Dominion;
using Dominion.Strategy;
using Dominion.Strategy.Description;

namespace Program
{
    public static class StrategyOptimizer
    {
        public static void FindBestStrategyForGame(GameConfig gameConfig)
        {
            var initialDescription = new PickByPriorityDescription(new CardAcceptanceDescription[]
            {
                new CardAcceptanceDescription( Cards.Province, CountSource.Always, null, Comparison.Sentinel, 0),
                new CardAcceptanceDescription( Cards.Gold, CountSource.Always, null, Comparison.Sentinel, 0),
                new CardAcceptanceDescription( Cards.Silver, CountSource.Always, null, Comparison.Sentinel, 0)
            });

            Random random = new Random();

            Card[] supplyCards = gameConfig.GetSupplyPiles(2, random).Select(pile => pile.ProtoTypeCard).ToArray();

            var initialPopulation = Enumerable.Range(0, 10).Select(index => initialDescription).ToArray();
            var algorithm = new GeneticAlgorithm.GeneticAlgorithmPopulationAgainstSelf<PickByPriorityDescription, MutatePickByPriorityDescription, ComparePickByPriorityDescription>(
                initialPopulation,
                new MutatePickByPriorityDescription(random, supplyCards),
                new ComparePickByPriorityDescription(),
                new Random());

            for (int i = 0; i < 1000; ++i)
            {
                System.Console.WriteLine("Generation {0}", i);
                System.Console.WriteLine("==============", i);
                for (int j = 0; j < 10; ++j)
                {
                    algorithm.currentMembers[j].Write(System.Console.Out);
                    System.Console.WriteLine();
                }

                algorithm.RunOneGeneration();

                System.Console.WriteLine();
            }
        }

        public static PlayerAction FindBestBigMoneyWithCardVsStrategy(PlayerAction playerAction, Card card, bool logProgress = false)
        {
            Random random = new Random();
            var initialPopulation = Enumerable.Range(0, 10).Select(index => new BigMoneyWithCardDescription(card)).ToArray();

            var algorithm = new GeneticAlgorithm.GeneticAlgorithmAgainstConstant<BigMoneyWithCardDescription, MutateDescriptionFromParameters, CompareBigMoneyWithCardDescription>(
                initialPopulation,
                new MutateDescriptionFromParameters(random),
                new CompareBigMoneyWithCardDescription(playerAction),
                new Random());

            BigMoneyWithCardDescription result = new BigMoneyWithCardDescription(card);
            double maxScore = -100;
            double lastMaxScore = maxScore;
            int countScoreUnchanged = 0;

            for (int i = 0; i < 1000; ++i)
            {
                if (logProgress)
                {
                    System.Console.WriteLine("Generation {0}", i);
                    System.Console.WriteLine("==============", i);
                }
                algorithm.RunOneGeneration();
                for (int j = 0; j < 10; ++j)
                {
                    double currentScore = algorithm.nextMembers[j].species.GetScoreVs(playerAction, showReport:logProgress);
                    if (currentScore > maxScore)
                    {
                        maxScore = currentScore;
                        result = algorithm.nextMembers[j].species;
                    }
                    if (logProgress)
                    {
                        algorithm.nextMembers[j].species.Write(System.Console.Out);
                        System.Console.WriteLine();
                    }
                }

                if (lastMaxScore == maxScore)
                {
                    if (countScoreUnchanged++ >= 3)
                        break;
                }
                else
                {
                    lastMaxScore = maxScore;
                    countScoreUnchanged = 0;
                }

                if (logProgress)
                {
                    System.Console.WriteLine();
                }
            }

            result.Write(System.Console.Out);
            System.Console.WriteLine();

            return result.ToPlayerAction();
        }
    }
}
