using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.TenPinBowling
{
    public class BowlingGame
    {
        private readonly IBowlingFrame[] bowlingFrames = new IBowlingFrame[10];

        public BowlingGame()
        {
            for (var i = 0; i < 9; i++)
            {
                bowlingFrames[i] = new NonFinalBowlingFrame();
            }

            bowlingFrames[9] = new FinalBowlingFrame();
        }

        public int CurrentFrameNumber { get; private set; } = 1;

        public bool IsGameFinished => bowlingFrames[9].IsComplete;

        public void AddRoll(Roll roll)
        {
            var currentFrame = bowlingFrames[CurrentFrameNumber - 1];

            currentFrame.AddRoll(roll);

            if (currentFrame.IsComplete && !IsGameFinished)
            {
                CurrentFrameNumber++;
            }
        }
    }
}
