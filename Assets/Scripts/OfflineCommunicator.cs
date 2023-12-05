using System;
using System.Collections;
using Proyecto26;
using UnityEngine;
using UnityEngine.Networking;
using Openworld.Models;
using System.Linq;

namespace Openworld
{

    /**
    **  This is a stub for the Communicator class that is used when the game is in offline mode.
    **  To utilize this, set the offlineMode flag in GameManager to true.
    **  Then implement the methods from Communicator that you need to use.
    **  The methods here should return stubbed out data rather than attempt to communicate with the server.
    */
    public class OfflineCommunicator : Communicator
    {
        public OfflineCommunicator(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        static PlayerDetailResponse _player = new PlayerDetailResponse()
        {
            id = "1",
            username = "Player1",
            lastSeenAt = DateTime.Now - TimeSpan.FromDays(1),
            email = "player1@email.com"
        };
        public override void Register(string email, string password, string username, Action<ResponseHelper> success, Action<RequestException> error)
        {
            _player.username = username;
            _player.lastSeenAt = DateTime.Now - TimeSpan.FromDays(1);
            _player.email = email;
            success(new ResponseHelper(new UnityWebRequest()));
        }

        public override void Login(string username, string password, Action<LoginResponse> success, Action<RequestException> error)
        {
            _player.username = username;
            _player.lastSeenAt = DateTime.Now - TimeSpan.FromDays(1);
            success(new LoginResponse()
            {
                player = _player.id,
                token = "1234"
            });
        }

        public override void GetPlayerDetail(string playerId, Action<PlayerDetailResponse> success, Action<RequestException> error)
        {
            _player.id = playerId;
            success(_player);
        }

        static Skill[] _skills = new Skill[]{
            new Skill { id = "1", name = "Archery", description = "Mastery in the use of bows and arrows." },
            new Skill { id = "2", name = "Alchemy", description = "Knowledge of magical potions and elixirs." },
            new Skill { id = "3", name = "Swordsmanship", description = "Expertise in wielding swords in combat." },
            new Skill { id = "4", name = "Elemental Magic", description = "Control over elemental forces like fire, water, earth, and air." },
            new Skill { id = "5", name = "Stealth", description = "The ability to move silently and remain unnoticed." },
            new Skill { id = "6", name = "Telekinesis", description = "Manipulation of objects using the power of the mind." },
            new Skill { id = "7", name = "Healing", description = "Skill in mending wounds and restoring health." },
            new Skill { id = "8", name = "Enchanting", description = "Adding magical properties to items through spells." },
            new Skill { id = "9", name = "Beast Mastery", description = "The ability to tame and control mythical beasts." },
            new Skill { id = "10", name = "Illusionism", description = "Creating illusions and deceiving the senses." }
        };
        public override void GetSkills(Action<Skill[]> success, Action<RequestException> error)
        {
            StartCoroutine(DoAfterDelay(1, () => { success(_skills); }));
        }

        static RacesResponse[] _races = new RacesResponse[5]
        {
            new RacesResponse { id = 1, name = "Elf", description = "Graceful and long-lived woodland being.", skills = new CharacterSkill[] { new CharacterSkill() { id = _skills[0].id, name = _skills[0].name, level = 1, description = _skills[0].description } } },
            new RacesResponse { id = 2, name = "Dwarf", description = "Skilled miner and craftsman with a love for mountains.", skills = new CharacterSkill[] { new CharacterSkill() { id = _skills[1].id, name = _skills[1].name, level = 1, description = _skills[1].description } } },
            new RacesResponse { id = 3, name = "Orc", description = "Fierce warrior known for strength and battle prowess.", skills = new CharacterSkill[] { new CharacterSkill() { id = _skills[2].id, name = _skills[2].name, level = 1, description = _skills[2].description } } },
            new RacesResponse { id = 4, name = "Merfolk", description = "Aquatic being with the upper body of a humanoid and lower body of a fish.", skills = new CharacterSkill[0] },
            new RacesResponse { id = 5, name = "Fairy", description = "Enchanting and magical creature with delicate wings.", skills = new CharacterSkill[0] }
        };
        public override void GetRaces(Action<RacesResponse[]> success, Action<RequestException> error)
        {
            StartCoroutine(DoAfterDelay(1, () => { success(_races); }));
        }

        static CharacterDetailResponse _character = new CharacterDetailResponse()
        {
            id = "1",
            name = "Sample Character",
            title = "Boss Master",
            race = _races.FirstOrDefault(r => r.id == 1).name,
            strength = 3,
            dexterity = 3,
            intelligence = 2,
            hp = 50f / 75f,
            mana = 50f / 100f,
            hunger = 75f / 100f,
            stamina = 90f / 100f,
            sleep = .75f,
            hpReplenish = 2,
            manaReplenish = 1,
            staminaReplenish = 1,
            maxHp = 75,
            maxMana = 100,
            maxStamina = 100,
            inventorySize = 10,
            portrait = "human_wizard_f",
            inventory = new Inventory()
        };
        public override void CreateCharacter(string gameId, string name, int raceId, int strength, int dexterity, int intelligence, CharacterSkill[] skills, Action<CharacterDetail> success, Action<RequestException> error)
        {
            _character = new CharacterDetailResponse()
            {
                id = "1",
                name = name,
                title = "Boss Master",
                race = _races.FirstOrDefault(r => r.id == raceId).name,
                strength = 3,
                dexterity = 3,
                intelligence = 2,
                hp = 50f / 75f,
                mana = 50f / 100f,
                hunger = 75f / 100f,
                stamina = 90f / 100f,
                sleep = .75f,
                hpReplenish = 2,
                manaReplenish = 1,
                staminaReplenish = 1,
                maxHp = 75,
                maxMana = 100,
                maxStamina = 100,
                inventorySize = 10,
                inventory = new Inventory(),
                portrait = "human_wizard_f",
                skills = skills
            };
            _games[0].character = _character;
            success(_character);
        }

        public override void GetCharacterDetail(string characterId, Action<CharacterDetailResponse> success, Action<RequestException> error)
        {
            // need to introduce this delay to give the binding time to initialize
            Debug.Log(_character);
            StartCoroutine(DoAfterDelay(.25f, () => { success(_character); }));
        }

        private IEnumerator DoAfterDelay(float seconds, Action doThis)
        {
            yield return new WaitForSeconds(seconds);
            doThis();
        }

        static GamesResponse[] _games = new GamesResponse[1]{
            new GamesResponse { game = new Game { id = "1", name = "SampleGame", owner = _player }, owner = true, character = _character}
        };
        public override void GetGames(Action<GamesResponse[]> success, Action<RequestException> error)
        {
            success(_games);
        }

        public override void CreateGame(string name, Action<Game> success, Action<RequestException> error)
        {
            var game = new Game()
            {
                id = "1",
                name = name,
                owner = _player
            };
            _games = new GamesResponse[1]
            {
                new GamesResponse { game = game, owner = true }
            };
            success(game);
        }
    }


}