# Combat Animation Setup

This project uses `CombatAnimationDriver` to drive animator parameters for attacks. To hook it up:

## Animator parameters
Create these parameters in your Animator Controller:
- **Int** `CombatState`
- **Float** `AttackDirX`
- **Float** `AttackDirY`

## Suggested state mapping
- `CombatState` values correspond to the enum order in `CombatStateMachine`:
  - 0 = Idle
  - 1 = Windup
  - 2 = Active
  - 3 = Recover
  - 4 = Cooldown

## Direction mapping
`AttackDirX`/`AttackDirY` are set to:
- Up: (0, 1)
- Down: (0, -1)
- Left: (-1, 0)
- Right: (1, 0)
- Neutral: (0, 0)

## Hooking up
1. Add `CombatAnimationDriver` to your fighter.
2. Assign the `CombatStateMachine` reference.
3. Map transitions in your Animator using the parameters above.

This keeps animation wiring data-driven while the combat state machine controls timing.

## No-animation fallback
If you do not have animations yet, add `CombatSpriteDirection` to your fighter. It flips the sprite on left/right attacks and tints the sprite during windup/active/recover to provide visual feedback.
