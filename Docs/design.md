# Design: Mount & Blade 2D (Unity 6)

## Core loops
1. **Travel** — Move across a 2D overworld with stamina cost and time-of-day modifiers.
2. **Recruit** — Interact with settlements to hire troops; cost and availability depend on relations.
3. **Fight** — Real-time combat with directional attacks/blocks, stamina and morale interplay.
4. **Grow** — Improve companions, upgrade troops, acquire gear, and manage wages.

## Systems overview
### World simulation
- **GameClock**: Discrete ticks driving economy updates and AI travel. Emits events for dawn/dusk to adjust lighting and spawn tables.
- **EventBus**: Lightweight pub/sub for decoupling UI, world events, and combat triggers.
- **SettlementNode**: Holds recruit pools, vendors, and relations. Provides interaction prompts.
- **PartyAIController**: Governs roaming parties (bandits, caravans) with states: idle, patrol, chase, flee.
- **EncounterSpawner**: Periodically spawns roaming parties using `EncounterTable`.
- **OverworldEncounterTrigger**: Switches to combat scenes when the player touches an encounter.

### Characters & parties
- **CharacterStats**: Encapsulates health, stamina, armor, damage, and a `DamageProfile` (slash/pierce/blunt).
- **PartyRoster**: List of units with wages, morale, and formation role (frontline/ranged/cavalry-equivalent).
- **MoraleSystem**: Derived from recent victories, wages paid, food status, and leadership perks.
- **StaminaComponent**: Spends stamina on attacks/blocks/sprints and regenerates over time; exhausted window prevents spending.
- **CurrencyWallet**: Tracks party gold and supports spend/add operations.
- **PartyWageSystem**: Pays daily wages on `GameClock` ticks and updates morale based on success.

### Combat
- **CombatStateMachine**: Handles states: Idle → Windup → Active → Recover → Cooldown. Supports queued feints and chamber blocks.
- **HitResolver**: Resolves overlap events between `Hitbox` and `Hurtbox` layers; applies armor mitigation curves and damage profiles.
- **Hitbox**: Weapon trigger collider that forwards overlaps to `HitResolver`.
- **CombatInputController**: Maps input actions (or legacy input) to start attacks.
- **CombatAIController**: Simple AI that attacks and blocks based on distance and timers.
- **CombatData**: ScriptableObject that stores combat timing and stamina costs.
- **Hurtbox**: Marker for body colliders that should receive damage.
- **StaminaSystem**: Costs for attacks/blocks/sprints; guard breaks when stamina is depleted during block.
- **Knockback & Poise**: Each weapon applies impulse; poise reduces stagger unless broken by heavy attacks.

### UI
- **HUDController**: Displays health, stamina, morale, party size, and time-of-day.
- **RecruitmentPanel**: Shows available troops with costs and expected wages; updates `PartyRoster` on confirm.
- **DialoguePanel**: Simple branching dialogues for lords/merchants.

## Content data (ScriptableObjects)
- `WeaponData`: damage profile, attack angles, windup/active/recover frames, stamina cost.
- `TroopData`: base stats, wage, upgrade target, formation role, equipment references.
- `FactionData`: colors, relations, recruit tables, banner sprite.
- `EncounterTable`: spawn rates for roaming parties based on region and time of day.

## Scenes
- `Scenes/Overworld`: Travel, interactions, and encounter triggers.
- `Scenes/Combat`: Arena spawned from overworld context; loads party compositions.
- `Scenes/Tests/*`: Isolated sandboxes (combat timing, AI paths, morale sim).

## Technical notes
- Use **FixedUpdate** for physics/overlaps in combat; drive overworld logic via a clock tick (`GameClock`).
- Keep `ScriptableObject` data stateless—runtime state lives in `MonoBehaviour` components.
- Prefer composition (components) over inheritance for character abilities (sprint, shield block, etc.).
- Centralize all layer masks and input actions in `Assets/Settings` to avoid magic numbers.

## Build slices (suggested order)
1. **Overworld movement slice**: Player + camera follow, collision bounds, interaction prompts for settlements.
2. **Combat core slice**: Two fighters, directional attacks/blocks, stamina drain, simple AI.
3. **Recruitment slice**: Settlement UI that consumes `TroopData`, updates `PartyRoster`, deducts gold.
4. **Morale & wages slice**: Daily tick paying wages, morale effects on flee chance and combat stamina regen.
5. **Save/load slice**: Serialize `PartyRoster`, gold, relations, and clock time to JSON.

## Risks & mitigations
- **Scope creep**: Limit to 2–3 troop archetypes initially; expand once loops feel good.
- **Animation workload**: Use 2D skeletal animation with reusable weapon swing curves; prototype with placeholders.
- **Balancing**: Build debug UI for tweaking stamina costs, poise, and armor values at runtime.
