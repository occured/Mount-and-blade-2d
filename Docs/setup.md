# Unity 6 Setup Guide (Comprehensive)

This guide walks through creating a playable Overworld → Encounter → Combat → Reward → Progression loop using the provided scripts.

## 0) Project prerequisites
- Unity 6 (2D URP)
- Input System package enabled

## 1) Create project + folders
1. Create a **2D (URP)** Unity 6 project.
2. Point it at this repo folder.
3. Ensure these folders exist (create if missing):
   - `Assets/Scenes`
   - `Assets/Prefabs`
   - `Assets/Settings`
   - `Assets/ScriptableObjects`

## 2) Enable Input System
1. **Project Settings → Player → Active Input Handling**: set to **Input System** or **Both**.
2. Assign the `Assets/Settings/InputActions.inputactions` asset to your `PlayerInput` (or use `PlayerInputBinder`).

## 3) Create layers
Add layers in **Project Settings → Tags and Layers**:
- `Hitbox`
- `Hurtbox`
- `Terrain`
- `Interaction`

## 4) Create ScriptableObjects
Create and populate these ScriptableObject assets in `Assets/ScriptableObjects`:
- **CharacterStats**: define player + troop stats (`characterId`, health, stamina, armor).
- **DamageProfile**: optional per-weapon damage multipliers.
- **WeaponData**: weapons with timing/damage.
- **TroopData**: set `baseStats`, `recruitCost`, `xpToUpgrade`, `upgradeTarget`.
- **TroopRegistry**: list all troop assets.
- **FactionData**: faction colors/banners.
- **EncounterTable**: add entries and a `troopPool` for each entry.
- **ItemData** + **ItemRegistry**: items and catalog.
- **QuestData** + **QuestRegistry**: quests and catalog.
- **LootTable**: weighted item drops.

## 5) Create prefabs
### 5.1 GameManager (global)
Create an empty **GameManager** prefab with:
- `GameClock`
- `EventBus`
- `GameState`
- `SaveSystem`

Wire references in **GameState**:
- `PartyRoster`, `CurrencyWallet`, `MoraleSystem`, `FoodInventory`, `PartyInventory`,
  `QuestLog`, `ReputationSystem`, `RenownSystem`, `PartyCapacity`.

Wire registries in **SaveSystem**:
- `CharacterRegistry`, `ItemRegistry`, `QuestRegistry`.

### 5.2 PartyManager (party systems)
Create a **PartyManager** prefab with:
- `PartyRoster`
- `PartyInventory`
- `CurrencyWallet`
- `MoraleSystem`
- `FoodInventory`
- `PartyWageSystem`
- `PartyFoodSystem`
- `PartyTravelSpeed`
- `PartyCapacity` + `PartyCapacityUpdater`
- `TroopUpgradeSystem`
- `PartyTroopProgression`
- `QuestLog`
- `QuestRewardSystem`
- `ReputationSystem`
- `RenownSystem`

### 5.3 Player (overworld)
Create a **Player** prefab with:
- `Rigidbody2D` + `Collider2D`
- `OverworldController`
- `OverworldInputReader`
- `SprintController`
- `PartyInventory` (if you want pickups directly on player)
- `PlayerInput` (or `PlayerInputBinder`)

Assign:
- `OverworldController.inputReader`
- `OverworldController.sprintController`
- `OverworldController.bounds` (optional)
- `OverworldController.targetCamera` (optional)

### 5.4 Enemy prefab (combat)
Create an **Enemy** prefab with:
- `Rigidbody2D` + `Collider2D`
- `HealthComponent`, `StaminaComponent`, `BlockController`
- `CombatStateMachine`, `CombatInputController` (or `CombatAIController`)
- `Hurtbox`
- Weapon child with `Hitbox`, `HitboxDirectionalOffsets`, `HitResolver`
- Optional: `CombatAnimationDriver`, `LootDropper`, `CombatRewardOnDeath`

### 5.5 Ally prefab (combat)
Create an **Ally** prefab (player party member) with similar components as the enemy, but driven by player input or AI.

## 6) Create scenes
### 6.1 Overworld scene
- `Grid` + `Tilemap`
- `Player` prefab
- `GameManager` prefab
- `PartyManager` prefab
- `OverworldCameraFollow` (on camera)
- `EncounterSpawner` + assign `EncounterTable`

### 6.2 Combat scene
- Empty root for **EnemySpawnRoot**
- Empty root for **AllySpawnRoot**
- `CombatSceneBootstrap` (assign enemy/ally prefabs + spawn roots)
- `CombatResolution` (set `overworldSceneName` to your Overworld scene)

## 7) Wire UI
Create a Canvas with panels:
- `HUDController` (assign health/stamina/morale/gold/food/renown labels)
- `RecruitmentPanel` (assign roster, wallet, recruits)
- `TroopUpgradePanel`
- `PartyInventoryPanel`
- `QuestLogPanel`
- `VendorPanel` (optional)

## 8) Encounter setup
Create an **Encounter** prefab:
- Collider2D (trigger)
- `EncounterParty` (configured automatically by spawner)
- `OverworldEncounterTrigger`
- `LootDropper` (assign LootTable)
- `CombatRewardOnDeath`

## 9) Test loop checklist
1. Move on overworld → trigger encounter.
2. Combat loads; enemies + allies spawn.
3. Kill enemies → rewards + XP; return to Overworld.
4. Recruit/upgrade troops; verify roster size and morale.
5. Save/Load via `SaveSystem` (add UI button if desired).

## 10) Troubleshooting
- **No input**: confirm `PlayerInput` is bound to InputActions or `PlayerInputBinder`.
- **No enemies**: ensure `EncounterTable` has `troopPool` entries.
- **No allies**: ensure `GameState.PartyRoster` is wired and roster has members.
- **No combat return**: ensure `CombatResolution` is in scene and enemy registration occurs.
