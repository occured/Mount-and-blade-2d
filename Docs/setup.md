# Unity 6 Setup Guide (Comprehensive)

This guide gets you to a runnable Overworld -> Encounter -> Combat -> Reward -> Progression loop using the provided scripts (scenes/prefabs are not committed, so you'll create them locally).

## 0) Project prerequisites
- Unity 6 (2D URP).
- Input System package enabled; in **Project Settings > Player > Active Input Handling** choose **Input System** or **Both**.
- Input actions: use `Assets/Settings/InputActions.inputactions` (`Gameplay/Move`, `Attack`, `Block`, `Sprint`) and assign it to `PlayerInput` or `PlayerInputBinder`.

## 1) Create project + folders
1. Create a **2D (URP)** Unity 6 project and point it at this repo folder.
2. Ensure these folders exist (create if missing):
   - `Assets/Scenes`
   - `Assets/Prefabs`
   - `Assets/Settings`
   - `Assets/ScriptableObjects`

## 2) Layers and tags
- Add layers in **Project Settings > Tags and Layers**: `Hitbox`, `Hurtbox`, `Terrain`, `Interaction`.
- Tag the player prefab as `Player` (required for `OverworldEncounterTrigger`).

## 3) Create ScriptableObjects (`Assets/ScriptableObjects`)
- **CharacterStats**: define player + troop stats; set a unique `characterId` for Save/Load and encounters.
- **CharacterRegistry**: include every `CharacterStats` entry used in encounters/roster.
- **DamageProfile** + **WeaponData**: optional per-weapon timing/damage multipliers.
- **TroopData**: set `baseStats`, `recruitCost`, `xpToUpgrade`, `upgradeTarget`.
- **TroopRegistry**: catalog of all troop assets for upgrades/progression.
- **FactionData**: faction colors/banners/relations.
- **EncounterTable**: entries with `faction`, `troopPool`, `minPartySize`, `maxPartySize`, `weight`.
- **ItemData** + **ItemRegistry**: item definitions and catalog for inventory/vendors/loot.
- **QuestData** + **QuestRegistry**: quest definitions and catalog.
- **LootTable**: weighted item drops.
- Optional: **CombatData** (timings/stamina cost) if you want per-weapon combat tuning.

## 4) Create prefabs
### 4.1 GameManager (global, DontDestroyOnLoad via `GameState`)
Components:
- `GameClock`
- `EventBus`
- `GameState`
- `SaveSystem`
- `AutosaveOnSceneChange` (optional)

Wire references:
- `GameState`: `PartyRoster`, `CurrencyWallet`, `MoraleSystem`, `FoodInventory`, `PartyInventory`, `QuestLog`, `ReputationSystem`, `RenownSystem`, `PartyCapacity`.
- `SaveSystem`: `GameState`, `CharacterRegistry`, `ItemRegistry`, `QuestRegistry`; optional `FactionData` list if you want reputation persisted.
- `AutosaveOnSceneChange`: `SaveSystem`.

### 4.2 PartyManager (party systems)
Components:
- `PartyRoster`
- `PartyInventory`
- `CurrencyWallet`
- `MoraleSystem`
- `FoodInventory`
- `PartyWageSystem` (hook `GameClock`, `PartyRoster`, `CurrencyWallet`, `MoraleSystem`)
- `PartyFoodSystem` (hook `GameClock`, `PartyRoster`, `FoodInventory`, `MoraleSystem`)
- `PartyTravelSpeed`
- `PartyCapacity` + `PartyCapacityUpdater` (hook `GameClock`)
- `TroopUpgradeSystem`
- `PartyTroopProgression` (hook `PartyRoster`, `TroopRegistry`, `TroopUpgradeSystem`)
- `QuestLog`
- `QuestRewardSystem`
- `ReputationSystem`
- `RenownSystem`

### 4.3 Player (overworld)
Components:
- `Rigidbody2D` + `Collider2D`
- `OverworldController` (assign `OverworldInputReader`, `SprintController`, optional `OverworldBounds`, `PartyTravelSpeed`, `targetCamera`, `EventBus`)
- `OverworldInputReader` (binds to Input Actions; can coexist with legacy input)
- `SprintController`
- `PartyInventory` (if you want pickups directly on player)
- `PlayerInput` or `PlayerInputBinder`
- Tag as `Player`

### 4.4 Enemy prefab (combat)
Components:
- `Rigidbody2D` + `Collider2D`
- `HealthComponent`, `StaminaComponent`, `BlockController`
- `CombatStateMachine`, `CombatInputController` (or `CombatAIController`)
- `Hurtbox`
- Weapon child with `Hitbox`, `HitboxDirectionalOffsets`, `HitResolver`
- Optional: `CombatAnimationDriver` or `CombatSpriteDirection`, `LootDropper`, `CombatRewardOnDeath`

### 4.5 Ally prefab (combat)
Same as the enemy prefab but driven by player input or AI. Add `CharacterStatsBinder` so stats can be applied at runtime.

### 4.6 Encounter prefab (overworld)
Components:
- Trigger `Collider2D`
- `EncounterParty` (populated by spawner)
- `OverworldEncounterTrigger` (set `combatSceneName`, keep `destroyOnTrigger` if you want one-shots)
- Optional: `LootDropper` + `LootTable`, `CombatRewardOnDeath`, `PartyAIController` (for roaming parties), `PartyLifetime`

## 5) Create scenes
### 5.1 Overworld scene
- Tilemap/Grid for terrain.
- Drop in **GameManager** prefab (DontDestroyOnLoad) and **PartyManager** prefab.
- Add **Player** prefab + camera with `OverworldCameraFollow`.
- Add `EncounterSpawner` (assign `EncounterTable` and `PartyAIController` prefab). Place any static encounters using the Encounter prefab above.

### 5.2 Combat scene
- Empty roots for **EnemySpawnRoot** and **AllySpawnRoot**.
- Add `CombatSceneBootstrap` (assign enemy/ally prefabs + spawn roots).
- Add `CombatResolution` (set `overworldSceneName`; assign `PartyTroopProgression` if you want XP on victory).
- Optional UI: `CombatResultUI` + victory panel + `SceneTransitionFader` for fade-out; `CombatDefeatUI` + defeat panel. Both listen to `CombatResolution` events.

## 6) Wire UI (Canvas)
- `HUDController`: assign health/stamina/morale/gold/food/renown labels and data sources.
- `RecruitmentPanel`: assign roster, wallet, recruits list, capacity, labels.
- `TroopUpgradePanel`
- `PartyInventoryPanel`
- `QuestLogPanel`
- `VendorPanel` (optional)
- `SaveLoadPanel` (assign `SaveSystem`)
- `SceneTransitionFader` (optional for scene fades)

## 7) Test loop checklist
1. Move in overworld and trigger an encounter.
2. Combat loads; enemies spawn from `EncounterContext` or fallback count; allies spawn from `GameState.PartyRoster`.
3. Kill enemies and verify rewards/XP; return to Overworld (or show defeat UI if all allies die).
4. Recruit/upgrade troops; verify roster size, morale, food, wages tick daily via `GameClock`.
5. Save/Load via `SaveSystem` (hooked to UI button or autosave).

## 8) Troubleshooting
- **No input**: confirm `PlayerInput` uses `InputActions.inputactions` or `PlayerInputBinder`.
- **No enemies**: ensure `EncounterTable` has `troopPool` entries and `OverworldEncounterTrigger` fires (player tagged).
- **No allies**: ensure `GameState.PartyRoster` reference is set and roster has members.
- **No combat return**: ensure `CombatResolution` is present and `CombatSceneBootstrap` registers enemies/allies.
- **Save/Load missing data**: make sure `SaveSystem` has `GameState` + registries and `FactionData` list if persisting reputation.
- **No combat animations**: see `Docs/animation.md` for Animator parameter setup.
