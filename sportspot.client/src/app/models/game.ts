export interface Game {
    id: string;
    teamId: string;
    gameStart: Date;
    attendingPlayerIds: string[];
    substitutionIds: string[];
  }