# HarmonyRailroader org and release setup plan

Every dependent mod runs this DLL, so one bad update could reach every player who uses them. The goal is that no single person or stolen account can ship a release alone.

Status: planning only. No org has been created, and the mod identifier has not changed.

## Names

- Org: `RoundhouseRR`.
- Mod name: `HarmonyRailroader`.
- Identifier: `roundhouserr/harmony-railroader`. Treat it as permanent, because dependent mods hardcode it.

## Decide before creating

- [ ] 2-3 owners, all known modders
- [ ] Who holds Nexus access (at least two people)

## GitHub org

- [ ] Require 2FA for all members.
- [ ] Keep to 2-3 owners. Add people slowly and remove inactive ones.
- [ ] All owners watch the repo, including releases.
- [ ] Keep the repo public. On the free plan, required environment reviewers and artifact attestations need a public repo.

## Repo protection

- [ ] Protect `main`: pull request required, one approval from someone else, no force-pushes, and no bypass for owners.
- [ ] Add a tag ruleset for release tags (`v*`): restrict creation, updates and deletion, so tags come only through the release workflow.
- [ ] Add a `release` environment: required reviewers, prevent self-review on, and bypass disallowed.
- [ ] Use a rule requiring code owner or last-push approval if needed, so an author can't approve their own final push.

Branch protection alone does not gate releases, because a maintainer could otherwise create a tag or GitHub Release without touching `main`. The tag ruleset and `release` environment are what enforce a second approval.

Known limit: an org owner can turn these rules off. They prevent mistakes and limit a stolen non-owner account, but trust in the owners still matters. Owner watch notifications are the backstop.

## Build and release

- [ ] Build only in GitHub Actions. Nobody uploads a file from their own PC.
- [ ] Download Harmony from NuGet with a pinned version and locked package hash.
- [ ] Check `0Harmony.dll` against the official NuGet package in the workflow, and fail if it differs.
- [ ] Publish SHA-256 hashes for the zip, `0Harmony.dll` and `HarmonyRailroader.dll`.
- [ ] Add artifact attestations with `actions/attest-build-provenance`. Users verify with `gh attestation verify <file> -R <org>`.
- [ ] Pin Actions to full commit SHAs and give workflows minimum permissions.
- [ ] Keep the loader small enough to review quickly, with no features beyond loading Harmony.

Open problem: the loader compiles against `Railroader.Modding.dll`, which is a game file and shouldn't be committed. Try a small stub assembly in the repo with the same assembly name and the interfaces the loader uses, and test the result in-game. Ask Giraffe Lab if a stub is acceptable.

## Distribution

- [ ] At least two people can update or remove the Nexus page.
- [ ] Upload only the zip built by Actions. A second owner checks its hash against the GitHub release.
- [ ] Check how Nexus and Vortex handle updates. We have not confirmed whether a bad release could spread automatically.

Known limit: GitHub's rules don't apply to Nexus. Anyone with upload access could post a file directly, so Nexus access needs the same care as owner access.

## Incident plan

1. Remove the bad release from GitHub and Nexus.
2. Post in Discord straight away: what's affected, what to do, and which version is safe.
3. Ship a clean version with a higher number, because players may otherwise stay on the bad one.
4. If an account was stolen: remove it, rotate credentials, and review recent changes and audit logs.
5. Write up what happened.

## Versioning

- Use the Harmony version plus a build number, for example `2.4.3.1`, so a loader fix doesn't look like a Harmony update.
- Mods require the lowest Harmony version they need.
- Test with a few popular mods before bumping Harmony.

## Before going public

- [ ] Test Goto, MapEnhancer and RailroaderModSettings alongside it.
- [ ] Confirm how `unscoped` affects saves and multiplayer.
- [ ] Ask Giraffe Lab whether it would ever ship Harmony with the game.
