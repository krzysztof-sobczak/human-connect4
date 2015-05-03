using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataGenerator
{
    [TestClass]
    public class DataGeneratorTest
    {
        String[] moves = { "1", "5555313322", "555555123312312233", 
                             "7777535544", "777777345534534455" };
        Board.FramePosition[] framePositions = { 
            Board.FramePosition.DOWN_LEFT, Board.FramePosition.DOWN_LEFT,
            Board.FramePosition.TOP_LEFT, Board.FramePosition.DOWN_RIGTH, 
            Board.FramePosition.TOP_RIGTH
        };

        Context[][] expectedContexts = {
            // 
            // moves = 1; <=> (0,0)
            new Context[] { 
                // -
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        3,
                        Board.DiskColor.ACT,
                        3,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        7,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        11,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        15,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        3),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        8,
                        Board.DiskColor.EMPTY,
                        4,
                        1),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        12,
                        Board.DiskColor.EMPTY,
                        4,
                        1),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        16,
                        Board.DiskColor.EMPTY,
                        4,
                        1),
                // |
                new Context(    // 8-my
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        3,
                        Board.DiskColor.ACT,
                        3,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        0),
                // /
                new Context(    // 13-sty
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        9,
                        Board.DiskColor.ACT,
                        3,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        1),
                // \
                new Context(    // 15-sty
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        9,
                        Board.DiskColor.EMPTY,
                        4,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        1)
            },
            
            // 
            // moves = 5555313322;
            new Context[] { 
                // -
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        3,
                        Board.DiskColor.EMPTY,
                        2,
                        3),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        6,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.ACT,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        1,
                        2,
                        Board.DiskColor.EMPTY,
                        1,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        2,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        3,
                        7,
                        Board.DiskColor.PAS,
                        3,
                        5),
                // |
                new Context(    // 8-my
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        3,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        2,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.PAS},
                        Context.ContextType.VERTICAL,
                        0,
                        0,
                        Board.DiskColor.EMPTY,
                        0,
                        2),
                // /
                new Context(    // 13-sty
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.PAS, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        4,
                        Board.DiskColor.PAS,
                        1,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.SLASH,
                        0,
                        3,
                        Board.DiskColor.EMPTY,
                        1,
                        4),
                // \
                new Context(    // 15-sty
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        5,
                        Board.DiskColor.ACT,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        4)
            },
            
            // 
            // moves = 555555123312312233;
            new Context[] { 
                // -
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        3,
                        Board.DiskColor.EMPTY,
                        2,
                        3),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        6,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.ACT,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        1,
                        2,
                        Board.DiskColor.EMPTY,
                        1,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        2,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        3,
                        7,
                        Board.DiskColor.PAS,
                        3,
                        5),
                // |
                new Context(    // 8-my
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        3,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        2,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.PAS},
                        Context.ContextType.VERTICAL,
                        0,
                        0,
                        Board.DiskColor.EMPTY,
                        0,
                        2),
                // /
                new Context(    // 13-sty
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.PAS, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        4,
                        Board.DiskColor.PAS,
                        1,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.SLASH,
                        0,
                        3,
                        Board.DiskColor.EMPTY,
                        1,
                        4),
                // \
                new Context(    // 15-sty
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        5,
                        Board.DiskColor.ACT,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        4)
            },
            
            // 
            // moves = 7777535544;
            new Context[] { 
                // -
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        3,
                        Board.DiskColor.EMPTY,
                        2,
                        3),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        6,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.ACT,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        1,
                        2,
                        Board.DiskColor.EMPTY,
                        1,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        2,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        3,
                        7,
                        Board.DiskColor.PAS,
                        3,
                        5),
                // |
                new Context(    // 8-my
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        3,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        2,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.PAS},
                        Context.ContextType.VERTICAL,
                        0,
                        0,
                        Board.DiskColor.EMPTY,
                        0,
                        2),
                // /
                new Context(    // 13-sty
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.PAS, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        4,
                        Board.DiskColor.PAS,
                        1,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.SLASH,
                        0,
                        3,
                        Board.DiskColor.EMPTY,
                        1,
                        4),
                // \
                new Context(    // 15-sty
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        5,
                        Board.DiskColor.ACT,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        4)
            },
            
            // 
            // moves = 777777345534534455;
            new Context[] { 
                // -
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        1,
                        3,
                        Board.DiskColor.EMPTY,
                        2,
                        3),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        2,
                        6,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.HORIZONTAL,
                        3,
                        10,
                        Board.DiskColor.EMPTY,
                        4,
                        6),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        0,
                        1,
                        Board.DiskColor.ACT,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        1,
                        2,
                        Board.DiskColor.EMPTY,
                        1,
                        2),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.ACT},
                        Context.ContextType.HORIZONTAL,
                        2,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.HORIZONTAL,
                        3,
                        7,
                        Board.DiskColor.PAS,
                        3,
                        5),
                // |
                new Context(    // 8-my
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        3,
                        Board.DiskColor.PAS,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        2,
                        Board.DiskColor.EMPTY,
                        2,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        1,
                        Board.DiskColor.EMPTY,
                        1,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.EMPTY, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.VERTICAL,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        4,
                        5),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.PAS, 
                        Board.DiskColor.ACT, Board.DiskColor.PAS},
                        Context.ContextType.VERTICAL,
                        0,
                        0,
                        Board.DiskColor.EMPTY,
                        0,
                        2),
                // /
                new Context(    // 13-sty
                    new Board.DiskColor[] {Board.DiskColor.PAS, Board.DiskColor.PAS, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.SLASH,
                        0,
                        4,
                        Board.DiskColor.PAS,
                        1,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.PAS},
                        Context.ContextType.SLASH,
                        0,
                        3,
                        Board.DiskColor.EMPTY,
                        1,
                        4),
                // \
                new Context(    // 15-sty
                    new Board.DiskColor[] {Board.DiskColor.EMPTY, Board.DiskColor.ACT, 
                        Board.DiskColor.EMPTY, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        5,
                        Board.DiskColor.ACT,
                        3,
                        7),
                new Context(
                    new Board.DiskColor[] {Board.DiskColor.ACT, Board.DiskColor.EMPTY, 
                        Board.DiskColor.PAS, Board.DiskColor.EMPTY},
                        Context.ContextType.BACKSLASH,
                        0,
                        4,
                        Board.DiskColor.EMPTY,
                        2,
                        4)
            }
        };


        public void AssertContext(Context expected, Context actual, String msg)
        {
            Assert.AreEqual(expected.contextType, actual.contextType, "contextType, " + msg);
            Assert.AreEqual(expected.crossingOpenedLines, 
                actual.crossingOpenedLines, "crossingOpenedLines, " + msg);
            Assert.AreEqual(expected.deep, actual.deep, "deep, " + msg);
            Assert.AreEqual(expected.missingDisk, actual.missingDisk, "missingDisk, " + msg);
            Assert.AreEqual(expected.row, actual.row, "row, " + msg);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(expected.values[i], actual.values[i],
                    "values[" + i.ToString() + ", " + msg);
            }
            Assert.AreEqual(expected.whoCanWin, actual.whoCanWin, "whoCanWin, " + msg);
        }


        [TestMethod]
        public void GenerateContextsForFrameTest()
        {
            for (int testID = 2; testID < expectedContexts.GetLength(0); testID++)
            {
                Board board = new Board();
                board.MakeMoves(DataConverter.StringToMoves(moves[testID]));

                DataGenerator gen = new DataGenerator(board);
                Context[] contexts = gen.GenerateContextsForFrame(framePositions[testID]);

                //
                for (int contextsIndex = 0; contextsIndex < expectedContexts.Length; contextsIndex++)
                {
                    AssertContext(expectedContexts[testID][contextsIndex], contexts[contextsIndex],
                        "testID=" + testID + "; contextsIndex=" + contextsIndex.ToString());
                }
            }
        }



    }
}
