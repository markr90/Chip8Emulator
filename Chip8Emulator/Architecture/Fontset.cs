
namespace Chip8Emulator.Architecture
{
    public static class Font
    {
		public static readonly long[] Set = new long[16] {
			0xF0909090F0, // 0
			0x2060202070, // 1
			0xF010F080F0, // 2
			0xF010F010F0, // 3
			0x9090F01010, // 4
			0xF080F010F0, // 5
			0xF080F090F0, // 6
			0xF010204040, // 7
			0xF090F090F0, // 8
			0xF090F010F0, // 9
			0xF090F09090, // A
			0xE090E090E0, // B
			0xF0808080F0, // C
			0xE0909090E0, // D
			0xF080F080F0, // E
			0xF080F08080  // F
		};
    }
}
