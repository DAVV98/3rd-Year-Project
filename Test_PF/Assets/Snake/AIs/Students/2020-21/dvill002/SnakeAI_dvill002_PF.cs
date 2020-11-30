using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using AlanZucconi.AI.BT;
using AlanZucconi.Snake;

[CreateAssetMenu
(
    fileName = "SnakeAI_dvill002_PF",
    menuName = "Snake/2020-21/SnakeAI_dvill002_PF"
)]
public class SnakeAI_dvill002_PF : SnakeAI
{
    

    public override Node CreateBehaviourTree(SnakeGame Snake)
    {
        int distance_tail = 1;
        double escape_Distance = 0;
        int previousSize = 0;
        int lastEaten = 0;

        int exit_loop = 750;

        // Select whther Snake has Tail or Snake has no tail.
        return new Selector
        (
            // If snake has no tail
            new Sequence
            (
                new Condition(() => Snake.Body.Count == 1),
                new Action(Snake.MoveTowardsFood)
            ),

            //If snake has tail
            new Sequence
            (
                
                // condition: check if snake has tail
                new Condition(() => Snake.Body.Count > 1),

                // Timer to get out of loops
                new Selector
                (

                    // Last eaten timer: Currently killing snake when it is on the edge!
                    new Sequence(
                        new Filter(
                            () => previousSize != Snake.Body.Count,
                            new Action(() =>
                            {
                                previousSize = Snake.Body.Count;
                                lastEaten = Snake.Ticks;

                            }
                            )
                        ),

                        // condition that results in false to continue selector
                        new Condition(() => Snake.Body.Count > 1000)
                    ),
                    
                    // Sequence that makes snake get out of loop
                    new Sequence(
                        new Condition(() => Snake.Ticks - lastEaten > exit_loop),                    
                        new Condition(Snake.IsFoodReachable),
                        new Action(Snake.MoveTowardsFood)
                    ),

                    // if tail is not reachable
                    new Sequence(

                        // conditions: check if tail is not reachable
                        new Sequence
                        (
                            new Condition(() => !Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x + distance_tail, Snake.TailPosition.y)))),
                            new Condition(() => !Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x - distance_tail, Snake.TailPosition.y)))),
                            new Condition(() => !Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y + distance_tail)))),
                            new Condition(() => !Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y - distance_tail))))
                        ),
                        new Selector(
                            new Filter(
                                Snake.IsFoodReachable,
                                new Action(Snake.MoveTowardsFood)
                            ),

                        // if tail is not reachable and food is not: turn left ot right
                            new Filter(
                                () => !Snake.IsFoodReachable(),
                                new Selector(
                                    new Sequence(
                                        new Condition(Snake.IsObstacleAhead),
                                        new Condition(Snake.IsFreeLeft),
                                        new Action(Snake.TurnLeft)
                                    ),
                                    new Sequence(
                                        new Condition(Snake.IsObstacleAhead),
                                        new Condition(Snake.IsFreeRight),
                                        new Action(Snake.TurnRight)
                                    ),
                                    new Sequence(
                                        new Condition(() => Snake.IsObstacleAhead()),
                                        new Condition(Snake.IsFreeLeft),
                                        new Action(Snake.TurnLeft)
                                    ),
                                    new Sequence(
                                        new Condition(() => Snake.IsObstacleAhead()),
                                        new Condition(Snake.IsFreeRight),
                                        new Action(Snake.TurnRight)
                                    )
                                )
                            )   
                        )
      
                    ),

                    // if food and tail reachable: go to food 
                    new Sequence
                    (
                       

                        new Condition(Snake.IsFoodReachable),
                        new Selector
                        (
                           new Condition(() => Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x + distance_tail, Snake.TailPosition.y)))),
                           new Condition(() => Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x - distance_tail, Snake.TailPosition.y)))),
                           new Condition(() => Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y + distance_tail)))),
                           new Condition(() => Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y - distance_tail))))

                        ),
                        new Action(Snake.MoveTowardsFood)

                    ),

                    // if tail cannot reach food or food is not reachable: follow tail
                    new Sequence(

                        // select: food not reachable || tail cannt reach food
                        new Selector(

                            // check if tail can reach food
                            new Sequence(
                                 
                                // condition: is food reachable
                                 new Condition(Snake.IsFoodReachable),
                                 // conditions: can tail reach food
                                 new Selector
                                 (
                                    new Condition(() => !Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x + distance_tail, Snake.TailPosition.y)))),
                                    new Condition(() => !Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x - distance_tail, Snake.TailPosition.y)))),
                                    new Condition(() => !Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y + distance_tail)))),
                                    new Condition(() => !Snake.IsReachable(Snake.FoodPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y - distance_tail))))
                                )
                            ),

                            // condition: is food reachable?
                            new Sequence(
                                new Condition(() => !Snake.IsFoodReachable())
                            )
                        ),

                       // action: go to tail
                       new Selector(
                           new Sequence(
                               new Condition(() => Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x + distance_tail, Snake.TailPosition.y)))),
                               new Action(() => Snake.MoveTowards(new Vector2Int(Snake.TailPosition.x + 1, Snake.TailPosition.y)))
                           ),
                           new Sequence(
                               new Condition(() => Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x - distance_tail, Snake.TailPosition.y)))),
                               new Action(() => Snake.MoveTowards(new Vector2Int(Snake.TailPosition.x - 1, Snake.TailPosition.y)))
                           ),
                           new Sequence(
                               new Condition(() => Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y + distance_tail)))),
                               new Action(() => Snake.MoveTowards(new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y + distance_tail)))
                           ),
                           new Sequence(
                               new Condition(() => Snake.IsReachable(Snake.HeadPosition, (new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y - distance_tail)))),
                               new Action(() => Snake.MoveTowards(new Vector2Int(Snake.TailPosition.x, Snake.TailPosition.y - distance_tail)))
                           )
                       )

                    ) 
                 )
             )
           
        );
    }
}


