using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
  public class FightService : IFightService
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    //could inject iCharacter service here, get character, would need authenticated user for this
    public FightService(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
    {
      var response = new ServiceResponse<AttackResultDto>();

      try
      {
        var attacker = await _context.Characters!
        .Include(character => character.Weapon)
        .FirstOrDefaultAsync(character => character.Id == request.AttackerId);

        var opponent = await _context.Characters!
        .FirstOrDefaultAsync(character => character.Id == request.OpponentId);

        int damage = DoWeaponAttack(attacker, opponent);

        if (opponent.HitPoints <= 0)
          response.Message = $"{opponent.Name} has been defeated!";

        await _context.SaveChangesAsync();

        response.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = opponent.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage
        };
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    private static int DoWeaponAttack(Character? attacker, Character opponent)
    {
      int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength)); // take the weapon damage and add random number between 0 and character strength
      damage -= new Random().Next(opponent!.Defense!); // subtract opponent defense from damage to get actual damage!

      if (damage > 0)
        opponent.HitPoints -= damage; // subtract opponent hp from damage
      return damage;
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
    {
      var response = new ServiceResponse<AttackResultDto>();

      try
      {
        var attacker = await _context.Characters!
        .Include(character => character.Skills)
        .FirstOrDefaultAsync(character => character.Id == request.AttackerId);

        var opponent = await _context.Characters!
        .FirstOrDefaultAsync(character => character.Id == request.OpponentId);

        var skill = attacker.Skills.FirstOrDefault(skill => skill.Id == request.SkillId);

        if (skill == null)
        {
          response.Success = false;
          response.Message = $"{attacker.Name} doesn't know this skill.";
          return response;
        }

        int damage = DoSkillAttack(attacker, opponent, skill);

        if (opponent.HitPoints <= 0)
          response.Message = $"{opponent.Name} has been defeated!";

        await _context.SaveChangesAsync();

        response.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = opponent.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage
        };
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    private static int DoSkillAttack(Character? attacker, Character opponent, Skill? skill)
    {
      int damage = skill.Damage + (new Random().Next(attacker.Intelligence)); // take the skill damage and add random number between 0 and character intelligence
      damage -= new Random().Next(opponent!.Defense!); // subtract opponent defense from damage to get actual damage!

      if (damage > 0)
        opponent.HitPoints -= damage; // subtract opponent hp from damage
      return damage;
    }

    public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
    {
      var response = new ServiceResponse<FightResultDto>
      {
        Data = new FightResultDto()
      };
      try
      {
        var characters = await _context.Characters!
        .Include(c => c.Weapon)
        .Include(c => c.Skills)
        .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();

        bool defeated = false;
        while (!defeated)
        {
          foreach (var attacker in characters)
          {
            var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
            var opponent = opponents[new Random().Next(opponents.Count)];

            int damage = 0;
            string attackedUsed = string.Empty;

            bool useWeapon = new Random().Next(2) == 0;
            if (useWeapon)
            {
              attackedUsed = attacker.Weapon.Name;
              damage = DoWeaponAttack(attacker, opponent);
            }
            else
            {
              var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
              attackedUsed = skill.Name;
              damage = DoSkillAttack(attacker, opponent, skill);
            }

            response.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackedUsed} with {(damage >= 0 ? damage : 0)} damage.");
            if (opponent.HitPoints <= 0)
            {
              defeated = true;
              attacker.Victories++;
              opponent.Defeats++;
              response.Data.Log.Add($"{opponent.Name} has been defeated!");
              response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
              break;
            }
          }
        }

        characters.ForEach(c =>
        {
          c.Fights++;
          c.HitPoints = 100;
        });

        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {

        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
    {
      var characters = await _context.Characters!
      .Where(c => c.Fights > 0)
      .OrderByDescending(c => c.Victories)
      .ThenBy(c => c.Defeats).ToListAsync();

      var response = new ServiceResponse<List<HighScoreDto>>
      {
        Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
      };

      return response;
    }
  }
}